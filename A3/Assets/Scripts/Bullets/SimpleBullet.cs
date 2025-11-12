using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public float Speed = 30;
    public float LifeSpan = 1;
    public static int BulletDamage = 1;


    private void Start()
    {
        Destroy(gameObject, LifeSpan);
    }

    void Update()
    {
        transform.Translate(0, 0, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        IDamagable enemy = collider.GetComponent<IDamagable>();
        if (enemy != null)
        {
            // Hit a Damagable Object
            enemy.Damage(BulletDamage);
            Destroy(gameObject);
        }
    }
}
