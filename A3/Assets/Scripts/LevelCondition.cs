using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCondition : MonoBehaviour
{
    public static LevelCondition Instance;

    [SerializeField] public int numberOfCastles = 0;
    [SerializeField] public int numberOfEnemies = 0;
    [SerializeField] public int CollectTheTreasure = 0;

    private void Awake()
    {
        Instance = this; // Inserting this into the Static Pigeon hole.
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    public int GetNumberOfEnemies() => numberOfEnemies;
    public int GetNumberOfSpawns() => numberOfCastles;
    public int GetNumberOfTreasures() => CollectTheTreasure;

    public void countSpawns()
    {
        numberOfCastles++;
    }

    public void countEnemies()
    {
        numberOfEnemies++;
    }

    public void countTreasures()
    {
        CollectTheTreasure++;
    }


    public void killSpawns()
    {
        numberOfCastles--;
        if (numberOfEnemies == 0 && numberOfCastles == 0 && CollectTheTreasure == 1)
        {
            GameManager.Instance.youWin = true;
            Debug.Log("All enemies Elliminated");
        }
    }

    public void killEnemies()
    {
        numberOfEnemies--;
        if (numberOfEnemies == 0 && numberOfCastles == 0 && CollectTheTreasure == 1)
        {
            GameManager.Instance.youWin = true;
            Debug.Log("Got The Treasure!!!");
        }
    }

    public void getTreasures()
    {
        CollectTheTreasure++;
        if (numberOfEnemies == 0 && numberOfCastles == 0 && CollectTheTreasure == 1)
        {
            GameManager.Instance.youWin = true;
            Debug.Log("All enemies Elliminated");
        }
    }

    private void FixedUpdate()
    {
        WinLevel();
        LoseLevel();
    }

    public virtual void WinLevel()
    {
        if (GameManager.Instance.youWin == true)
        {
            if (!UIManager.Instance.OpenReplace(GameUIID.YouWin))
            {
                UIManager.Instance.OpenReplace(GameUIID.YouWin);
                Debug.Log("You Win!!!");
            }
            this.GetComponent<LevelCondition>().enabled = false;
        }
    }

    public virtual void LoseLevel()
    {
        if (GameManager.Instance.youLose == true)
        {
            if (!UIManager.Instance.OpenReplace(GameUIID.YouLose))
            {
                UIManager.Instance.OpenReplace(GameUIID.YouLose);
                Debug.Log("You Lose...");
            }
            this.GetComponent<LevelCondition>().enabled = false;
        }
    }
}
