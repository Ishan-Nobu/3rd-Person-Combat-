using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{   
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");
    private const float crossFadeDuration = 0.1f;
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTargetCancel;
        stateMachine.InputReader.AttackEvent += OnAttack;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {   
        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
        }
        
        if (stateMachine.Targeter.CurrentTarget ==  null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalcMovement(deltaTime);
        Move(movement * stateMachine.TargetedMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);
        FaceTarget();
    }

    public override void Exit()
    {   
        stateMachine.InputReader.TargetEvent -= OnTargetCancel;
        stateMachine.InputReader.AttackEvent -= OnAttack;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
    private void OnAttack()
    {
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
    }
    private void OnTargetCancel()
    {   
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    private void OnDodge()
    {   
        if (stateMachine.InputReader.MovementValue == Vector2.zero) { return; }
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementValue));
    }
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }
    private Vector3 CalcMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();
        
        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        
        return movement;
    }
    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.1f, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value, 0.1f, deltaTime);
        }
    }
}
    
