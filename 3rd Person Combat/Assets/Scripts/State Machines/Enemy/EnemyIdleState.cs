using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{   
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float crossFadeDuration = 0.1f;
    private const float animatorDampTime = 0.1f;
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { } 

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        FacePlayer();
        if (IsInChangeRange())
        {
            Debug.Log("In range");
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 0, animatorDampTime, deltaTime);
    }
    public override void Exit()
    {
        
    }
    private void FacePlayer()
    {
        if (GetDistance() <= stateMachine.LookAtRange * stateMachine.LookAtRange)
        {
            FaceTarget();
        }
    }
}
