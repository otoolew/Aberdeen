using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBaseFSM : StateMachineBehaviour
{
    public GameObject unitObject;
    public UnitBrain unitBrain;
    public NavMeshAgent navAgent;
    public UnitWeaponBehavior unitWeapon;
    public UnitHUD unitHUD;
    public Transform unitTarget;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unitObject = animator.gameObject;       
        navAgent = unitObject.GetComponent<NavMeshAgent>();
        unitBrain = unitObject.GetComponent<UnitBrain>();
        unitWeapon = unitObject.GetComponent<UnitWeaponBehavior>();
        unitHUD = unitBrain.GetComponent<UnitHUD>();
        unitTarget = unitBrain.CurrentTarget;

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (unitBrain.TargetInSight())
        {
            Debug.Log("Target Insight!");
            animator.transform.LookAt(unitBrain.CurrentTarget);
            animator.SetBool("HasTarget", true);
        }
        if (unitTarget != null)
        {
            animator.transform.LookAt(unitTarget);
            unitBrain.TargetDistance = Vector3.Distance(unitTarget.position, unitTarget.transform.position);
            animator.SetFloat("TargetDistance", unitBrain.TargetDistance);
        }
        else
        {
            unitBrain.TargetDistance = 1000f;
            animator.SetFloat("TargetDistance", unitBrain.TargetDistance);
        }
    }
}
