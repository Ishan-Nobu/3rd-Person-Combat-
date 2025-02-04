using System;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{   
    private readonly int BlockHash =  Animator.StringToHash("Block");
    private const float crossFadeDuration = 0.1f;
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) {  }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, crossFadeDuration);
        stateMachine.Health.SetInvulnerable(true);
    }
    public override void Tick(float deltaTime)
    {   
        Move(deltaTime);
        if (!stateMachine.InputReader.IsBlocking)
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
    }
    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }
}
