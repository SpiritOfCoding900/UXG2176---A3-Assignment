using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IPrototypeState
{
    const float DelayDeath = 2f;
    float _Count;
    public DeadState(FSMProtoType machine) : base(machine)
    {

    }

    public override PrototypeStateType Type => PrototypeStateType.Dead;

    public override void OnStateEnter()
    {
        _Count = DelayDeath;
        base.OnStateEnter();
    }

    public override void OnStateUpdate()
    {
        DyingHPZero();
    }

    protected void DyingHPZero()
    {
        _Count -= Time.deltaTime;
        if (_Count <= 0)
        {
            GameObject.Destroy(Machine.gameObject);
            GameManager.Instance.youLose = true;
        }

        //--------- Or Just Use ---------//

        // GameObject.Destroy(Machine.gameObject, DelayDeath);
    }

    protected void DeadZero()
    {

    }
}
