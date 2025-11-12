using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[Serializable]
public class SpawnLimit
{
    public float minX = -1f;
    public float maxX = 1f;
    public float minY = -1f;
    public float maxY = 1f;
    public float minZ = -1f;
    public float maxZ = 1f;
}
public class Spawn : MonoBehaviour, IDamagable
{
    public static float maxSpawnHealth = 10;
    public float spawnHealth;

    // Image For HealthBar.
    [SerializeField] private Image hpImage;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text maxHpText;

    [SerializeField] public SpawnLimit spawnLimit;
    
    public Enemy enemyPrefab;
    public float startingSpawnTimer = 4;
    public float spawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnHealth = maxSpawnHealth;

        // Count the number of spawns.
        LevelCondition.Instance.countSpawns();

        spawnTimer = startingSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.HasOpened) return;

        SpawnHPDisplay();
        SpawnDead();
        SpawnEnemies();
    }

    protected void SpawnHPDisplay()
    {
        hpImage.fillAmount = spawnHealth / maxSpawnHealth;

        hpText.text = spawnHealth.ToString();
        maxHpText.text = "/ " + maxSpawnHealth.ToString();
    }

    protected void SpawnEnemies()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(spawnLimit.minX, spawnLimit.maxX), UnityEngine.Random.Range(spawnLimit.minY, spawnLimit.maxY), UnityEngine.Random.Range(spawnLimit.minZ, spawnLimit.maxZ));
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            spawnTimer = startingSpawnTimer;
        }
    }

    public void Damage(int damage)
    {
        spawnHealth -= damage;
        Debug.Log(name + " took " + RayCastGun.BulletDamage + " damage.");
    }

    protected virtual void SpawnDead()
    {
        if (spawnHealth == 0)
        {
            // Spawn deducted in the count.
            LevelCondition.Instance.killSpawns();

            // Destroy this gameObject.
            Destroy(gameObject);
        }
    }
}
