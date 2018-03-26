using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ScanState performs the scanning action on a unit
/// </summary>
public class ScanState : UnitBaseFSM
{
    float timer;
    float cooldown = 2f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        //.Log("Enter Scan State");
        timer = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        timer += Time.deltaTime;
        //Debug.Log("Animator HasTarget() == " + animator.GetBool("HasTarget"));
        if (timer < cooldown)
        {
            //Debug.Log("No Threat in view... Continue to Scan!");
        }
        else if (unitBrain.CurrentNode.lastNode == true)
        {
            //Debug.Log("Nowhere Go!! So I am Chilling here!");
        }
        else
        {
            //Debug.Log("Moving to Next WayPoint");
            //animator.SetBool("IsAdvancing", true);
            animator.SetBool("HasTarget", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //navAgent.SetDestination(player.transform.position);
    }
}
