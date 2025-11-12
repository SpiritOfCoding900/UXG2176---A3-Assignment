using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;



[Serializable]
public class PlayerLimit
{
    public float minX = -1f;
    public float maxX = 1f;
    public float minY = -1f;
    public float maxY = 1f;
    public float minZ = -1f;
    public float maxZ = 1f;
}

public class FSMProtoType : MonoBehaviour, IMyGameEvent
{
    Dictionary<PrototypeStateType, IPrototypeState> _states = new Dictionary<PrototypeStateType, IPrototypeState>();

    PrototypeStateType _currentStateType = PrototypeStateType.Unassigned;

    bool _isActive = true;

    [HideInInspector]
    public NavMeshAgent Agent;
    public static int MaxHealth = 100;
    public float Health = 0;
    public RayCastGun Gun;  
    public float moveSpeed;
    public Vector3 moveDirection;
    public static float horizontalInput;
    public static float verticalInput;
    public Rigidbody _rb;

    [SerializeField] public PlayerLimit playerLimit;

    public bool isShooting = false;
    //public bool isJumping = true;

    public Transform playerBody;

    public static FSMProtoType Instance;

    public IPrototypeState CurrentState
    {
        get
        {
            if (_currentStateType != PrototypeStateType.Unassigned)
            {
                return _states[_currentStateType];
            }
            else
            {
                return null;
            }
        }
    }

    public void OnGameResume()
    {
        _isActive = true;
    }

    public void OnGamePause()
    {
        _isActive = false;
    }

    //Register State
    public void RegisterState(IPrototypeState state)
    {
        _states[state.Type] = state;
    }

    //Change state
    public void ChangeState(PrototypeStateType nextStateType)
    {
        //Check it current state is not next state, if yes, return!
        if (_currentStateType == nextStateType) return;

        //Get next state from _states
        //If does not exist, throw warning!
        if (!_states.TryGetValue(nextStateType, out IPrototypeState nextState))
        {
            Debug.LogWarning($"{nextStateType} is not a registered state.");
            return;
        }

        IPrototypeState prevState = CurrentState;
        prevState?.OnStateExit();

        nextState?.OnStateEnter();

        //switch to next state type
        _currentStateType = nextStateType;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        Gun = GameManager.Instance.CurrentGun;

        Health = MaxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Agent = this.GetComponent<NavMeshAgent>();

        //RegisterState(new LookState(this));
        RegisterState(new MoveState(this));
        RegisterState(new AttackState(this));
        RegisterState(new DeadState(this));

        ChangeState(PrototypeStateType.Navigate);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }


    // Update is called once per frame
    void Update()
    {
        if (!_isActive) return;

        if (Health == 0)
        {
            ChangeState(PrototypeStateType.Dead);
        }


        CurrentState?.OnStateUpdate();
        PlayerMapLimit();
    }

    protected void PlayerMapLimit()
    {
        float clampx = Mathf.Clamp(transform.position.x, playerLimit.minX, playerLimit.maxX);
        float clampz = Mathf.Clamp(transform.position.z, playerLimit.minZ, playerLimit.maxZ);
        transform.position = new Vector3(clampx, 0, clampz);
    }

    public void PlayerDamage(int damage)
    {
        Health -= damage;
        Debug.Log(name + " took " + SimpleEnemyBullet.BulletDamage + " damage.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // isJumping = true;
        }
    }
}

public abstract class IPrototypeState
{
    protected FSMProtoType Machine;
    public abstract PrototypeStateType Type { get; }

    public IPrototypeState(FSMProtoType machine)
    {
        Machine = machine;
    }

    public virtual void OnStateEnter()
    {
        Debug.Log($"Changing to {this.GetType()}");
    }
    public virtual void OnStateUpdate() { }
    public virtual void OnStateExit() { }
}

public enum PrototypeStateType
{
    Unassigned,
    Looking,
    Navigate,
    Shoot,
    Attack,
    Dead
}
