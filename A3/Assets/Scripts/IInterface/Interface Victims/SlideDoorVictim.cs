using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoorVictim : MonoBehaviour
{
    public static SlideDoorVictim Instance;

    private void Awake()
    {
        Instance = this; // Inserting this into the static pigeon hole.
    }

    private void OnDestroy() // Give Score After Destroyed.
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
        
    }

}
