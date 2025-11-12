using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBullet : MonoBehaviour
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
        FSMProtoType player = collider.GetComponent<FSMProtoType>();
        if (player != null)
        {
            // Hit a Damagable Object
            player.PlayerDamage(BulletDamage);
            Destroy(gameObject);
        }
    }
}
