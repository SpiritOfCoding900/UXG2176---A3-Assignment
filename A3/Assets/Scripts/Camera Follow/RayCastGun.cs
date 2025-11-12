using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst.CompilerServices;

public class RayCastGun : MonoBehaviour
{
    public static int BulletDamage = 1;     
    public LayerMask maskTarget;
    public LayerMask maskDoor;      
    public LayerMask maskPlatform;
    public LayerMask maskTreasure;

    [SerializeField] private Image hpImage;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text enemyName;



    // Start is called before the first frame update
    void Start()
    {
        hpImage.fillAmount = 1 / 1;
        hpText.text = "0";
        enemyName.text = "non";
    }

    // Update is called once per frame
    void Update()
    {
        // ShootingWithRaycast();
    }

    public void ShootingWithRaycast()
    {
        // Define the starting point to end of the Raycast
        Ray ray = new Ray(transform.position, transform.forward);

        // Perform the Raycast and store the information about the hit object
        RaycastHit hitInfo;

        // Shoot Enemy
        if (Physics.Raycast(ray, out hitInfo, 100f, maskTarget, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            // The Raycast hit an object, do something with the hit object
            Debug.Log("Hit object: " + hitInfo.collider.gameObject.name);

            // Gun Sound
            AudioManager.Instance.PlaySound(SoundID.Gunshot);

            // Hit a Damagable Object
            hitInfo.collider.GetComponent<IDamagable>().Damage(BulletDamage);

            enemyName.text = hitInfo.collider.gameObject.name;
            hpImage.fillAmount = hitInfo.collider.GetComponent<Enemy>().enemyHealth / Enemy.maxEnemyHealth;
            hpText.text = hitInfo.collider.GetComponent<Enemy>().enemyHealth.ToString();
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100f, Color.green);
        }

        // Open Door
        if (Physics.Raycast(ray, out hitInfo, 100f, maskDoor, QueryTriggerInteraction.Ignore))
        {
            if(Input.GetMouseButton(0))
            {
                // The Raycast hit an object, do something with the hit object
                Debug.Log("Interacted object: " + hitInfo.collider.gameObject.name);

                // Hit the door
                hitInfo.collider.GetComponent<ISlideDoor>().SlideDoorOpen(true);
            }
        }

        // Move Platform
        if (Physics.Raycast(ray, out hitInfo, 100f, maskPlatform, QueryTriggerInteraction.Ignore))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Hit the platform
                if(MyMovingPlatform.Instance.platformIsRight != true)
                {
                    hitInfo.collider.GetComponent<ISwitchPlatform>().SwitchPlatform(true);
                }
                else
                {
                    hitInfo.collider.GetComponent<ISwitchPlatform>().SwitchPlatform(false);
                }
            }    
        }

        // Collect Treasure
        if (Physics.Raycast(ray, out hitInfo, 100f, maskTreasure, QueryTriggerInteraction.Ignore))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Hit the treasure
                hitInfo.collider.GetComponent<ITreasure>().TreasureCollected();
            }
        }
    }
}
