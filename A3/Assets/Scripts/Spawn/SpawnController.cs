using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public MapLimit GameLimits;

    public GameObject[] Enemies;

    public float spawnTimer;
    float maxSpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnTimer = spawnTimer;

    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimeDelta();

        if (spawnTimer <= 0)
        {
            SpawnBox();
            spawnTimer = maxSpawnTimer;
        }
    }

    void SpawnTimeDelta()
    {
        spawnTimer -= Time.deltaTime;
    }


    public void SpawnBox()
    {
        Vector3 myPos = new Vector3(Random.Range(GameLimits.minX, GameLimits.maxX), Random.Range(GameLimits.minY, GameLimits.maxY), Random.Range(GameLimits.minZ, GameLimits.maxZ));

        GameObject BoxesDropped = Instantiate(Enemies[Random.Range(0, Enemies.Length)], myPos, Quaternion.identity);
    }
}
