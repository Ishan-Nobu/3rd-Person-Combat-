using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PlayerTestState : PlayerBaseState
{   
    private float timer;
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {   
        stateMachine.InputReader.JumpEvent += OnJump;
        Debug.Log("Enter");
    }

    public override void Tick(float deltaTime)
    {
        timer = timer + deltaTime;
        Debug.Log(timer);
    }   

    public override void Exit()
    {   
        stateMachine.InputReader.JumpEvent -= OnJump;
        Debug.Log("Exit");
    }    

    public void OnJump()
    {   
        Debug.Log("Jumping");
        stateMachine.SwitchState(new PlayerTestState(stateMachine)); 
    }
}
