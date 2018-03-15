using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancingState : UnitBaseFSM
{
    WayPoint[] wayPoints;
    int currentWP;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Debug.Log("Enter Advancing State");
        unitHUD.ChangeText("Advancing State!");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   // WayPoint Check
        if (unitBrain.WayPointList.Count <= 0)
        {
            animator.SetBool("IsDefending", true);
            return;
        }
        Transform waypoint = unitBrain.WayPointList[0].transform;

        navAgent.SetDestination(wayPoints[currentWP].transform.position);
        var lookPos = wayPoints[currentWP].transform.position - unitObject.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        unitObject.transform.rotation = rotation;
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            unitBrain.WayPointList.Remove(waypoint);
        }

        navAgent.SetDestination(wayPoints[currentWP].transform.position);


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAdvancing", false);
        //navAgent.SetDestination(player.transform.position);
    }
}
