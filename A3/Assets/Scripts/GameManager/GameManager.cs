using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SimpleSingleton<GameManager>
{
    List<IMyGameEvent> _handlers = new List<IMyGameEvent>();

    public void RegisterHandler(IMyGameEvent handler) => _handlers.Add(handler);
    public void UnregisterHandler(IMyGameEvent handler) => _handlers.Remove(handler);

    void ExecuteEvent(Action<IMyGameEvent> onExecute)
    {
        foreach (IMyGameEvent handler in _handlers.ToArray())
        {
            if (handler.gameObject == null)
            {
                UnregisterHandler(handler);
                continue;
            }
        }
        //onExecute(handler);
    }

    public void StartGame() => ExecuteEvent((handler) => handler.OnGameStart());
    public void PauseGame() => ExecuteEvent((handler) => handler.OnGamePause());
    public void ResumeGame() => ExecuteEvent((handler) => handler.OnGameResume());
    public void GameOver() => ExecuteEvent((handler) => handler.OnGameOver());

    
    [Header("Player Data: ")]
    [SerializeField]
    private FSMProtoType player01;
    public FSMProtoType CurrentPlayer { private set; get; }

    [Header("Gun Data: ")]
    [SerializeField]
    private RayCastGun Gun01;
    public RayCastGun CurrentGun { private set; get; }


    public bool youWin;
    public bool youLose;

    void Awake()
    {
        // Ensure only one instance of GameManager exists
        base.Awake(); // only if SimpleSingleton has its own Awake()

        // Detect gun when this scene loads
        SceneManager.sceneLoaded += OnSceneLoaded;

        FindingTheGun();
    }

    public void FindingTheGun()
    {
        // Find the main camera
        Camera mainCam = Camera.main;

        if (mainCam != null)
        {
            // Try to get RayCastGun component from it
            RayCastGun gun = mainCam.GetComponent<RayCastGun>();
            if (gun != null)
            {
                Gun01 = gun;
                putGunOnPlayer(gun); // optional if you want to do something extra here
                Debug.Log("[GameManager] RayCastGun registered from Main Camera.");
            }
            else
            {
                Debug.LogWarning("[GameManager] No RayCastGun found on Main Camera.");
            }
        }
        else
        {
            Debug.LogWarning("[GameManager] No Main Camera found in scene!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("[GameManager] Scene loaded: " + scene.name);
        FindingTheGun();
    }

    // Start is called before the first frame update
    void Start()
    {
        youWin = false;
        youLose = false;
        UIManager.Instance.Open(GameUIID.Logo);
        //AudioManager.Instance.PlaySound(SoundID.BGMusic);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void spawnPlayerOnce(Vector3 PlayersNewSpawnPosition)
    {
        // Spawn the player once when entering a Level.
        Vector3 playerPos = PlayersNewSpawnPosition;

        FSMProtoType obj = Instantiate(player01, playerPos, Quaternion.identity);
        if (obj.TryGetComponent(out FSMProtoType player))
        {
            CurrentPlayer = player;
        }
    }

    public void putGunOnPlayer(RayCastGun getGun)
    {
        CurrentGun = getGun;
        if (CurrentPlayer != null)
        {
            Debug.Log("[GameManager] Gun equipped on player.");
        }
    }
}

public interface IMyGameEvent
{
    GameObject gameObject { get; }

    void OnGameStart() { }
    void OnGamePause() { }
    void OnGameResume() { }
    void OnGameOver() { }
    void OnGameStop() { }
}
