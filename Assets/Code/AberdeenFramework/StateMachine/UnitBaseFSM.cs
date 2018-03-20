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

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unitObject = animator.gameObject;       
        navAgent = unitObject.GetComponent<NavMeshAgent>();
        unitBrain = unitObject.GetComponent<UnitBrain>();
        unitWeapon = unitObject.GetComponent<UnitWeaponBehavior>();
        unitHUD = unitBrain.GetComponent<UnitHUD>();
    }
}
