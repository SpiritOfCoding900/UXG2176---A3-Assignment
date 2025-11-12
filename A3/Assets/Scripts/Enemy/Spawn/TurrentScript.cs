using UnityEditor;
using UnityEngine;

public class TurrentScript : MonoBehaviour
{
    public enum TurrentStates
    {
        Idle,
        See,
        Shoot,
    }

    public TurrentStates turrentState;

    public static TurrentScript Instance;

    public FSMProtoType Player01;

    public LayerMask targetMask;
    public bool sawPlayer, attackPlayer;
    public float sightRadius, attackRadius;
    public float sightRadiusCircle, attackRadiusCircle;

    protected void Awake()
    {
        Instance = this;
    }

    protected void OnDestroy()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {

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
            turrentState = TurrentStates.Idle;
            transform.rotation = new Quaternion(180f, 0f, 0f, 0f);
        }

        if (sawPlayer && !attackPlayer)
        {
            turrentState = TurrentStates.See;
        }

        if (sawPlayer && attackPlayer)
        {
            turrentState = TurrentStates.Shoot;
        }
        
        SeeThePlayer();
    }

    protected void SeeThePlayer()
    {
        if(turrentState == TurrentStates.See || turrentState == TurrentStates.Shoot)
        {
            if (Player01 != null)
            {
                gameObject.transform.LookAt(Player01.transform.position);
            }
        }
    }
}


