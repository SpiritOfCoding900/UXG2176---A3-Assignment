using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTreasure : MonoBehaviour, ITreasure
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TreasureCollected()
    {
        // Treasure increased in the count.
        LevelCondition.Instance.getTreasures();
        // Destroy this gameObject.
        Destroy(gameObject);
    }
}
