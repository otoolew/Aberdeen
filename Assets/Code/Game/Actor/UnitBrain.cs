using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBrain : MonoBehaviour
{
    Animator anim;
    NavMeshAgent navAgent;
    UnitWeapon unitWeapon;
    UnitVision unitVision;

    public string Name { get; set; }
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        unitWeapon = GetComponent<UnitWeapon>();
        unitVision = GetComponent<UnitVision>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetNavAgentTarget(Transform target)
    {
        navAgent.SetDestination(target.position);
    }
    public void ShoutOut()
    {
        Debug.Log("OUCH!");
    }

}
