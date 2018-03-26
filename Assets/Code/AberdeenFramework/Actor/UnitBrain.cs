using ActionGameFramework.Health;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
/// <summary>
/// UnitBrain Class controls unit logic
/// </summary>
public class UnitBrain : MonoBehaviour
{
    public string UnitName { get; set; }
    private Animator animator;
    private NavMeshAgent navAgent;
    private UnitWeaponBehavior unitWeapon;
    private UnitHUD unitHUD;
    private Ray ray;
    private RaycastHit rayHit;
    private SphereCollider visionCollider;
    public List<Transform> VisableTargetList;
    public Transform CurrentTarget;
    public float TargetDistance;
    public LayerMask TargetMask;
    public Transform ObjectivePoint;
    public float UnitVisionRange;
    public float UnitVisionRadius;
    public float UnitAttackRange;
    public StringVariable TeamName;
    public LayerMask BlockingMask;
    public UnityEvent OnEnemySighted;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        unitWeapon = GetComponent<UnitWeaponBehavior>();
        unitHUD = GetComponent<UnitHUD>();
        visionCollider = GetComponent<SphereCollider>();
        VisableTargetList = new List<Transform>();
    }

    public void SetNavAgentTarget(Transform target)
    {
        navAgent.SetDestination(target.position);
    }

    public bool TargetInSight()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, UnitVisionRadius, TargetMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            transform.LookAt(hitColliders[i].transform);
            ray.origin = transform.position;
            ray.direction = transform.forward;
            //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(ray.origin, ray.direction * UnitVisionRange, Color.green);
            if (Physics.Raycast(ray, out rayHit, UnitVisionRange, TargetMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * UnitVisionRange, Color.red);
                CurrentTarget = rayHit.collider.gameObject.transform;
                //Debug.Log("Found Target!");
                Debug.Log("Current Target Set " + CurrentTarget);
                animator.SetBool("HasTarget", true);
                transform.LookAt(CurrentTarget);
                if (CurrentTarget != null)
                    return true;
            }
            i++;
        }
        CurrentTarget = null;
        return false;
    }

    /// <summary>
    /// The event that fires off when a unit has been destroyed
    /// </summary>
    public Action unitDestroyed;

    /// <summary>
    /// Sets the node to navigate to
    /// </summary>
    /// <param name="node">The node that the agent will navigate to</param>
    public void SetWayPoint(Transform waypoint)
    {
        ObjectivePoint = waypoint;
    }

    public void EnemySighted()
    {
        Debug.Log("I have sighted an Enemy!");
    }
    public void LostEnemySight()
    {
        Debug.Log("I have LOST sight of an Enemy!");
    }

}
