using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public LevelCondition myConditionScripts;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.spawnPlayerOnce(new Vector3(0, 0, -150));
        myConditionScripts.GetComponent<LevelCondition>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
