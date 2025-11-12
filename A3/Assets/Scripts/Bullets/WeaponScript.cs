using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject Bullet01;
    public float FireRate = 0.2f;
    float canShootAgain;


    // Update is called once per frame
    void Update()
    {
        if (Time.time > canShootAgain)
        {
            canShootAgain = Time.time + FireRate;
            GameObject bullet = Instantiate(Bullet01, transform.position, transform.rotation);
        }
    }
}
