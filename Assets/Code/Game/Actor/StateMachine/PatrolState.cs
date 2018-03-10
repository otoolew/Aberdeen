using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : UnitBaseFSM {
    GameObject[] wayPoints;
    int currentWP;

    void Awake()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // Go to WayPoint 0;
        currentWP = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   // WayPoint Check   
        if (wayPoints.Length == 0) return;

        // Distance Check
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            currentWP++;
            if (currentWP >= wayPoints.Length)
            {
                currentWP = 0;
            }
        }

        navAgent.SetDestination(wayPoints[currentWP].transform.position);
        // Rotate GameObject towards target
        var lookPos = wayPoints[currentWP].transform.position - unit.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        unit.transform.rotation = rotation;
        //Debug.Log(enemy.name + " Patrol State " + currentWP + " of "+ wayPoints.Length);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //navAgent.SetDestination(player.transform.position);
    }
}
