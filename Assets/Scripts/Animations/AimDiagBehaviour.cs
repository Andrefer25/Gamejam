using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDiagBehaviour : StateMachineBehaviour
{
    private bool _isAimingDiag;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _isAimingDiag = false;
        animator.SetFloat("AimDiagAnimation", 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_isAimingDiag)
        {
            if (stateInfo.normalizedTime % 1 > 0.98f)
            {
                _isAimingDiag = true;
                animator.SetFloat("AimDiagAnimation", 1);
            }
        }
    }
}
