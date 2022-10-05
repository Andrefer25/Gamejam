using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimUpBehaviour : StateMachineBehaviour
{
    private bool _isAimingUp;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _isAimingUp = false;
        animator.SetFloat("AimUpAnimation", 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_isAimingUp)
        {
            if (stateInfo.normalizedTime % 1 > 0.98f)
            {
                _isAimingUp = true;
                animator.SetFloat("AimUpAnimation", 1);
            }
        }
    }
}
