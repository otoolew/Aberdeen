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
    public string Name { get; set; }
    Animator anim;
    NavMeshAgent navAgent;
    UnitWeaponBehavior unitWeapon;
    UnitHUD unitHUD;
    Ray ray;
    RaycastHit rayHit;
    public List<Transform> VisableTargetList = new List<Transform>();
    public WayPointLane unitLane;
    public Transform CurrentTarget;
    public Node CurrentNode;
    public Vector3 Destination;
    public float TargetDistance;
    public LayerMask TargetMask;
    public float UnitVisionRange;
    public float UnitVisionRadius;
    public StringVariable teamName;
    public LayerMask VisionMask;
    public UnityEvent OnEnemySighted;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        unitWeapon = GetComponent<UnitWeaponBehavior>();
        unitHUD = GetComponent<UnitHUD>();
        InitWayPath();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * UnitVisionRange;
        if (CurrentTarget != null)
        {
            Debug.DrawRay(transform.position, forward, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, forward, Color.green);
        }


    }
    public void SetNavAgentTarget(Transform target)
    {
        navAgent.SetDestination(target.position);
    }

    public void FindTargetInVision()
    {
        RaycastHit hit;
        Vector3 p1 = transform.position;
        if (Physics.SphereCast(p1, UnitVisionRadius, transform.forward, out hit, UnitVisionRange, TargetMask))
        {
            ray.origin = transform.position;
            ray.direction = transform.forward;

            if (Physics.Raycast(ray, out rayHit, UnitVisionRange, VisionMask))
            {
                Debug.Log("RayHit " + rayHit.collider.name);
                CurrentTarget = hit.collider.gameObject.transform;
                //Debug.Log("Found Target!");
                anim.SetTrigger("Attack");
            }
        }
        else
        {
            CurrentTarget = null;
            //return to previous state;
        }
    }
    void FindTargetsInArea()
    {
        VisableTargetList.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, UnitVisionRadius, TargetMask);

        int i = 0;
        while (i < hitColliders.Length)
        {

            ray.origin = transform.position;
            ray.direction = transform.forward;
            //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(ray.origin, ray.direction * UnitVisionRange, Color.green);
            if (Physics.Raycast(ray, out rayHit, UnitVisionRange, TargetMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * UnitVisionRange, Color.red);
                CurrentTarget = rayHit.collider.gameObject.transform;
                //Debug.Log("Found Target!");
                anim.SetTrigger("Attack");
            }
            i++;
        }

    }

    /// <summary>
    /// Obsolete
    /// 
    /// </summary>
    void InitWayPath()
    {
        WayPointLane[] unitLanes = FindObjectsOfType<WayPointLane>();

        //WayPoint[] waypoints = GameObject.FindObjectsOfType<WayPoint>();
        foreach (var item in unitLanes)
        {
            if (item.WayPointTeam.Value == teamName.Value)
            {
                unitLane = item;
            }
        }
        CurrentNode = unitLane.StartingNode;
    }

    /// <summary>
    /// The event that fires off when a unit has been destroyed
    /// </summary>
    public Action unitDestroyed;

    /// <summary>
    /// Sets the node to navigate to
    /// </summary>
    /// <param name="node">The node that the agent will navigate to</param>
    public void SetNode(Node node)
    {
        CurrentNode = node;
    }
    /// <summary>
    /// Finds the next node in the path
    /// </summary>
    public void GetNextNode(Node currentlyEnteredNode)
    {
        // Don't do anything if the calling node is the same as the m_CurrentNode
        if (CurrentNode != currentlyEnteredNode)
        {
            return;
        }
        if (CurrentNode == null)
        {
            Debug.LogError("Cannot find current node");
            return;
        }

        Node nextNode = CurrentNode.GetNextNode();
        if (nextNode == null)
        {
            if (navAgent.enabled)
            {
                navAgent.isStopped = true;
            }
            return;
        }

        Debug.Assert(nextNode != CurrentNode);
        SetNode(nextNode);
        MoveToNode();
    }
    /// <summary>
    /// Moves the agent to a position in the <see cref="Agent.m_CurrentNode" />
    /// </summary>
    public void MoveToNode()
    {
        Vector3 nodePosition = CurrentNode.GetRandomPointInNodeArea();
        nodePosition.y = CurrentNode.transform.position.y;
        Destination = nodePosition;
        NavigateTo(Destination);
    }
    /// <summary>
    /// Set the NavMeshAgent's destination
    /// </summary>
    /// <param name="nextPoint">The position to navigate to</param>
    protected virtual void NavigateTo(Vector3 nextPoint)
    {
        if (navAgent.isOnNavMesh)
        {
            navAgent.SetDestination(nextPoint);
        }
    }
    public void EnemySighted()
    {
        Debug.Log("I have sighted an Enemy");
    }
}
