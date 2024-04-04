using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{   
    private readonly int FreeLookHash = Animator.StringToHash("FreeLookSpeed");
    private const float animatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {   
        stateMachine.InputReader.TargetEvent += OnTarget;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = MovementCalc();

        stateMachine.CharacterController.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookHash, 0, animatorDampTime, deltaTime);
            return;
        }
        FaceMovementDirection(movement, deltaTime);
        stateMachine.Animator.SetFloat(FreeLookHash, 1, animatorDampTime, deltaTime);
    }

    public override void Exit()
    {   
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }    
    
    private void OnTarget()
    {   
        if (!stateMachine.Targeter.SelectTarget()) { return; }
        
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
    private Vector3 MovementCalc() // CAMERA RELATIVE MOVEMENT
    {
        Vector3 _camForward = stateMachine.MainCameraTranform.forward;
        Vector3 _camRight = stateMachine.MainCameraTranform.right;

        _camForward.y = 0;
        _camRight.y = 0;

        _camForward.Normalize();
        _camRight.Normalize();

        return _camForward * stateMachine.InputReader.MovementValue.y + _camRight * stateMachine.InputReader.MovementValue.x;
    }
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), 
        deltaTime * stateMachine.SmoothRotationValue);
    }
}
