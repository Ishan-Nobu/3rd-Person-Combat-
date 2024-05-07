using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();

    protected float GetNormalizedTime(Animator animator, string tag) // To check how far we're through the animation
    {
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextStateInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextStateInfo.IsTag(tag))
        {
            return nextStateInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentStateInfo.IsTag(tag))
        {
            return currentStateInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
