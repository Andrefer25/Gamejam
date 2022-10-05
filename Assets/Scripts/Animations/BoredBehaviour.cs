using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoredBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _timeUntilBored;
    private bool _isBored;
    private float _idleTime;
    private int _boredAnimation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!_isBored)
        {
            _idleTime += Time.deltaTime;

            if(_idleTime > _timeUntilBored && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _isBored = true;
                _boredAnimation = 1;
            }
        }
        else if(stateInfo.normalizedTime % 1 > 0.98f)
        {
            ResetIdle();
        }
        animator.SetFloat("BoredAnimation", _boredAnimation, 0.2f, Time.deltaTime);

    }

    private void ResetIdle()
    {
        _isBored = false;
        _idleTime = 0;
        _boredAnimation = 0;
    }

}
