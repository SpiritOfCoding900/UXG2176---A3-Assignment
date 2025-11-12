using UnityEngine;

public class AttackState : IPrototypeState
{
    const float _attackDuration = 0.25f;
    float _currentAttackDuration;

    public AttackState(FSMProtoType machine) : base(machine)
    {

    }

    public override PrototypeStateType Type => PrototypeStateType.Attack;

    public override void OnStateEnter()
    {
        _currentAttackDuration = _attackDuration;
        Machine.Gun.ShootingWithRaycast();
        base.OnStateEnter();
    }

    public override void OnStateUpdate()
    {
        AttackingClick();
    }

    protected void AttackingClick()
    {
        _currentAttackDuration -= Time.deltaTime;
        if (_currentAttackDuration < 0)
        {
            Machine.ChangeState(PrototypeStateType.Navigate);
        }
    }
}
