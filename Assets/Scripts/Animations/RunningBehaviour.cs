using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBehaviour : StateMachineBehaviour
{
    private bool _isRunning;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _isRunning = false;
        animator.SetFloat("RunningAnimation", 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!_isRunning)
        {
            if (stateInfo.normalizedTime % 1 > 0.98f)
            {
                _isRunning = true;
                animator.SetFloat("RunningAnimation", 1);
            }
        }
    }
}
