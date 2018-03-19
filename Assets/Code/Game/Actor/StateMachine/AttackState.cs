using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : UnitBaseFSM {
    float timer;
    float cooldown = 1f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Debug.Log("Enter Attack State");
        unitHUD.ChangeText("Attacking State!");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        //navAgent.SetDestination(animator.transform.position);
        navAgent.isStopped = true;
        if(unitBrain.CurrentTarget != null)
        {
            //Debug.Log("I am Attacking!");
            animator.SetBool("HasTarget", true);
            unitObject.transform.LookAt(unitBrain.CurrentTarget);
            
            if(timer >= cooldown)
            {
                timer = 0;
                unitWeapon.FireWeapon();
            }
                
        }
        else
        {
            // Move to Advance State
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
