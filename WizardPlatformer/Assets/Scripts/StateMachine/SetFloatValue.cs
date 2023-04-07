using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatValue : StateMachineBehaviour
{
    public string floatName;
    public bool updateOnStateMachineEnter, updateOnStateMachineExit;
    public bool updateOnStateEnter, updateOnStateExit;
    public float enterValue, exitValue;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateOnStateEnter)
        {
            animator.SetFloat(floatName, enterValue);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnStateExit)
        {
            animator.SetFloat(floatName, exitValue);
        }    
    }

    override public void OnStateMachineEnter(Animator animator, int stateMachineHashPath)
    {
        if (updateOnStateMachineEnter)
        {
            animator.SetFloat(floatName, enterValue);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachineHashPath)
    {
        if (updateOnStateMachineExit)
        {
            animator.SetFloat(floatName, exitValue);
        }
    }
}
