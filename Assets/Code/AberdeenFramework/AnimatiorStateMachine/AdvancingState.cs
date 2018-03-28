using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancingState : UnitBaseFSM
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if(unitBrain.Destination == null)
        {
            
        }
        //Debug.Log("Enter Advancing State");
        //unitHUD.ChangeText("Advancing State!");
        navAgent.isStopped = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   // WayPoint Check
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
        navAgent.SetDestination(unitBrain.Destination);
        navAgent.isStopped = false;
        if (navAgent.remainingDistance < navAgent.stoppingDistance)
        {
            //Debug.Log("Stopping Distance State");
            if (unitBrain.CurrentNode.lastNode == true)
            {
                //Debug.Log("Last Node Distance State");
                //animator.SetBool("IsScanning", true);
                animator.SetBool("HasNextNode", false);
                navAgent.SetDestination(unitBrain.Destination);
            }
        }
     
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAdvancing", false);
        //navAgent.SetDestination(player.transform.position);
    }
}
