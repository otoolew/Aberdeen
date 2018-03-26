using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancingState : UnitBaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Debug.Log("Enter Advancing State");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   // WayPoint Check
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        navAgent.SetDestination(unitBrain.ObjectivePoint.position);
        if (navAgent.remainingDistance < navAgent.stoppingDistance)
        {
            Debug.Log("At Objective");
            navAgent.SetDestination(unitBrain.transform.position);
            animator.SetBool("IsAdvancing", false);
        }
        else
        {
            animator.SetBool("IsAdvancing", true);
        }      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
