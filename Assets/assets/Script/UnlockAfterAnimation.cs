using UnityEngine;

public class UnlockAfterAnimation : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Force unlock when exiting hit state
        animator.SetBool(AnimationStrings.lockVelocity, false);
        animator.SetBool(AnimationStrings.canMove, true);
        Debug.Log("UnlockAfterAnimation: Unlocking player movement");
    }
}