using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{   
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float crossFadeDuration = 0.1f;
    private float impactDuration = 1f;
    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine) {  }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {   
        Move(deltaTime);
        
        impactDuration -= deltaTime;

        if(impactDuration <= 0f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
