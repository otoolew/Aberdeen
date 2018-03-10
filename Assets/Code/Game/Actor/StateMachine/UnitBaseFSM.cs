using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBaseFSM : StateMachineBehaviour
{
    public GameObject unit;
    public NavMeshAgent navAgent;
    public UnitWeapon unitWeapon;
    public UnitVision unitVision;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.gameObject;       
        navAgent = unit.GetComponent<NavMeshAgent>();
        unitWeapon = unit.GetComponent<UnitWeapon>();
        unitVision = unit.GetComponent<UnitVision>();
    }
}
