using System.Collections;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{   
    private bool alreadyAppliedforce;
    private float previousFrameTime;
    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {   
        attack = stateMachine.Attacks[attackIndex];
    }
    public override void Enter()
    {   
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        //stateMachine.InputReader.AttackEvent += AttackDown;
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }
    
    public override void Tick(float deltaTime)
    {   
        Move(deltaTime);
        FaceTarget();
        AttackDown();
    }

    public override void Exit()
    {
        //stateMachine.InputReader.AttackEvent -= AttackDown;
    }

    private void AttackDown()
    {   
        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if (normalizedTime >= previousFrameTime)
        {         
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        if (!stateMachine.InputReader.IsAttacking)
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {   
                Debug.Log("switching to target state");
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {   
                Debug.Log("switching to freelook state");
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previousFrameTime = normalizedTime;
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState
        (
            new PlayerAttackingState
            (
                stateMachine,
                attack.ComboStateIndex
            )
        );
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedforce) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.ForceIntensity);

        alreadyAppliedforce = true;
    }

    
}