using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{   
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float crossFadeDuration = 0.1f;
    private float impactDuration = 1f;
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine) {  }

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
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        
    }
}
