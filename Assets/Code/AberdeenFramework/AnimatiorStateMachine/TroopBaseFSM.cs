using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TroopBaseFSM : StateMachineBehaviour
{
    public GameObject unitObject;
    public TroopAgent troopAgent;
    public NavMeshAgent navAgent;
    public UnitWeaponBehavior unitWeapon;
    public UnitHUD unitHUD;
    public Transform unitTarget;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unitObject = animator.gameObject;
        navAgent = unitObject.GetComponent<NavMeshAgent>();
        troopAgent = unitObject.GetComponent<TroopAgent>();
        unitWeapon = unitObject.GetComponent<UnitWeaponBehavior>();
    }
}
