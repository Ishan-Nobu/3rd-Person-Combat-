using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{   
    private readonly int ClimbHash = Animator.StringToHash("Climb");
    private const float crossFadeDuration = 0.1f;
    private readonly Vector3 offset = new Vector3(0f, 2.325f, 0.65f);
    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)  {   }
    public override void Enter()
    {   
        stateMachine.Animator.CrossFadeInFixedTime(ClimbHash, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator, "Climbing") < 1f) { return; }

        stateMachine.CharacterController.enabled = false;

        stateMachine.transform.Translate(offset, Space.Self);

        stateMachine.CharacterController.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));
    }
    public override void Exit()
    {
        stateMachine.CharacterController.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }
}
