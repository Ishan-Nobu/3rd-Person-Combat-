using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void FaceTarget()
    {
        if (stateMachine.Player == null) { return; }

        Vector3 lookPos =  stateMachine.Player.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }
    protected bool IsInChangeRange()
    {   
        if (stateMachine.Player.IsDead) { return false; }
        
        return GetDistance() <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }
    protected float GetDistance()
    {
        float distance =  (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return distance;
    }
}
