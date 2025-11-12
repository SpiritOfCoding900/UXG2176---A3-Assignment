using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Enemy;

public class Enemy : MonoBehaviour, IDamagable
{
    public enum EnemyStates
    {
        Idle,
        See,
        Shoot,
    }
    
    public EnemyStates enemyState;

    public static Enemy Instance;

    public Rigidbody rig;
    public static float maxEnemyHealth = 3;
    public float enemyHealth;
    private NavMeshAgent agent;

    public FSMProtoType Player01;

    // Image For HealthBar.
    [SerializeField] private Image hpImage;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text maxHpText;

    // Player In Attack Range.
    public LayerMask targetMask;
    public bool sawPlayer, attackPlayer;
    public float sightRadius, attackRadius;
    public float sightRadiusCircle, attackRadiusCircle;
    public float walkSpeed = 10f;
    // Bullet Heaven.
    public GameObject Bullet;
    float timer_ = 0;
    int bulletsLeft_ = 20;
    int currentShot_ = 0;

    private void Awake()
    {
        Instance = this; // Inserting this into the Static Pigeon hole.
        agent = GetComponent<NavMeshAgent>();

    }

    private void OnDestroy()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = maxEnemyHealth;

        // Count the number of enemies.
        LevelCondition.Instance.countEnemies();

        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.HasOpened) return;

        EngagingPlayer();
    }

    protected void EngagingPlayer()
    {
        sightRadiusCircle = sightRadius;
        attackRadiusCircle = attackRadius;

        // sightRadius Triggered.
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, sightRadius, targetMask);
        sawPlayer = playerInRange.Length > 0;
        if (sawPlayer)
        {
            Player01 = playerInRange[0].GetComponent<FSMProtoType>();
        }

        // attackRadius Triggered.
        Collider[] playerInAttackRange = Physics.OverlapSphere(transform.position, attackRadius, targetMask);
        if (playerInAttackRange.Length > 0)
        {
            attackPlayer = true;
        }
        else attackPlayer = false;


        if (!sawPlayer && !attackPlayer)
        {
            enemyState = EnemyStates.Idle;
        }

        if (sawPlayer && !attackPlayer)
        {
            enemyState = EnemyStates.See;
        }


        if (sawPlayer && attackPlayer)
        {
            enemyState = EnemyStates.Shoot;
        }

        Stop();
        Move();
        BulletHeaven();

        EnemyDead();
        EnemyHPDisplay();
    }

    protected void BulletHeaven()
    {
        if (enemyState == EnemyStates.Shoot)
        {
            if (Player01 != null)
            {
                agent.SetDestination(Player01.transform.position);

                gameObject.GetComponent<NavMeshAgent>().speed = 0f;

                Vector3 relativePos = Player01.transform.position - transform.position;
                relativePos.y = 0;
                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = rotation;

                float TimePerShot = 0.3f;//time to wait after a bullet
                int ShotsPerBurst = 3;//fire 3 shots in a burst
                float TimePerBurst = 0.5f;//wait this time after a burst
                int MagazineSize = 20;//how many bullets the gun can store
                float ReloadTime = 2;//how much time to reload a magazine

                timer_ -= Time.deltaTime;
                if (timer_ <= 0)
                {
                    if (bulletsLeft_ > 0)//still have bullet to fire
                    {
                        if (currentShot_ < ShotsPerBurst)
                        {
                            //Fire
                            //muzzleFlash.Play();
                            AudioManager.Instance.PlaySound(SoundID.Gunshot);
                            Instantiate(Bullet, transform.position, transform.rotation);
                            currentShot_++;
                            bulletsLeft_--;
                            timer_ = TimePerShot;
                        }
                        else //a full round is fired
                        {
                            timer_ = TimePerBurst;
                            currentShot_ = 0;
                        }
                    }
                    else //no more bullet, need to reload
                    {
                        timer_ = ReloadTime;
                        currentShot_ = 0;//reset the burst
                        bulletsLeft_ = MagazineSize;
                    }
                }
            }
        }
    }

    protected void Move()
    {
        if(enemyState == EnemyStates.See)
        {
            if (Player01 != null)
            {
                gameObject.GetComponent<NavMeshAgent>().speed = walkSpeed;
                agent.SetDestination(Player01.transform.position);
                Vector3 relativePos = Player01.transform.position - transform.position;
                relativePos.y = 0;

                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = rotation;
            }
        }
    }

    protected void Stop()
    {
        if (enemyState == EnemyStates.Idle)
        {
            if (Player01 != null)
            {
                agent.SetDestination(Player01.transform.position);

                gameObject.GetComponent<NavMeshAgent>().speed = 0f;

                Vector3 relativePos = Player01.transform.position - transform.position;
                relativePos.y = 0;
                // the second argument, upwards, defaults to Vector3.up
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = rotation;
            }
        }
    }

    protected void EnemyHPDisplay()
    {
        hpImage.fillAmount = enemyHealth / maxEnemyHealth;

        hpText.text = enemyHealth.ToString();
        maxHpText.text = "/ " + maxEnemyHealth.ToString();
    }

    public void Damage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log(name + " took " + RayCastGun.BulletDamage + " damage.");
    }

    protected virtual void EnemyDead()
    {
        if (enemyHealth == 0)
        {
            // Enemy deducted in the count.
            LevelCondition.Instance.killEnemies();

            // Destroy this gameObject.
            Destroy(gameObject);
        }
    }
}

//[CustomEditor(typeof(Enemy))]
//public class CubeEditor : Editor
//{
//    void OnSceneGUI()
//    {
//        Enemy enemy = (Enemy)target;

//        Handles.color = Color.yellow;
//        Handles.DrawWireDisc(enemy.transform.position + (enemy.transform.up * -1.5f), new Vector3(0, 1, 0), enemy.sightRadiusCircle);
//        Handles.color = Color.red;
//        Handles.DrawWireDisc(enemy.transform.position + (enemy.transform.up * -1.5f), new Vector3(0, 1, 0), enemy.attackRadiusCircle);
//    }
//}
