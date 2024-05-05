using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{   
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float crossFadeDuration = 0.1f;
    private const float animatorDampTime = 0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, crossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {   
        if(!IsInChangeRange())
        {   
            Debug.Log("Out of range");    
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return; 
        }

        if (IsInAttackRange())
        {   
            Debug.Log("Attacking!!!");
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }
    
        MoveToPlayer(deltaTime);
        FaceTarget();
        stateMachine.Animator.SetFloat(SpeedHash, 1f, animatorDampTime, deltaTime);
    }
    public override void Exit()
    {   
        if(stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.ResetPath();
            stateMachine.Agent.velocity = Vector3.zero;
        } 
    }
    private void MoveToPlayer(float deltaTime)
    {   
        if(stateMachine.Agent.isOnNavMesh)
        {
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }
        stateMachine.Agent.velocity = stateMachine.CharacterController.velocity;
    }
    private bool IsInAttackRange()
    {   
        if(stateMachine.Player.IsDead) { return false; }
        
        return GetDistance() <= stateMachine.AttackRange * stateMachine.AttackRange;
    }
}
