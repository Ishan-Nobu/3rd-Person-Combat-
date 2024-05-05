using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{   
    private readonly int AttackHash = Animator.StringToHash("Attack1");
    private const float crossFadeDuration = 0.1f;
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)  {    }

    public override void Enter()
    {   
        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback); 
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {   
        FaceTarget();
        if (GetNormalizedTime(stateMachine.Animator) >= 1f)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }
    public override void Exit()
    {
        
    }
}
