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
    public WayPointLane UnitLane;
    public Transform CurrentTarget;
    public Node CurrentNode;
    public Vector3 Destination;
    public float TargetDistance;
    public LayerMask TargetMask;
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
        InitWayPath();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!VisableTargetList.Contains(CurrentTarget))
        //{
        //    FindTargetInVision();
        //}

        if (CurrentTarget != null)
        {
            TargetDistance = Vector3.Distance(transform.position, CurrentTarget.transform.position);
            animator.SetFloat("TargetDistance", TargetDistance);
        }
        else
        {
            FindTargetInVision();
            TargetDistance = 1000f;
            animator.SetFloat("TargetDistance", TargetDistance);
        }
    }
    public void SetNavAgentTarget(Transform target)
    {
        navAgent.SetDestination(target.position);
    }
    public void FindTargetInVision()
    {
        foreach (var target in VisableTargetList)
        {
            ray = new Ray
            {
                origin = transform.position,
                direction = transform.forward
            };

            if (Physics.Raycast(ray, out rayHit, UnitVisionRange, TargetMask))
            {
                //Debug.Log("RayHit " + rayHit.collider.name);
                CurrentTarget = rayHit.collider.gameObject.transform;
                Debug.Log("Current Target Set " + CurrentTarget);
                //Debug.Log("Found Target!");
                animator.SetBool("HasTarget", true);
                transform.LookAt(CurrentTarget);
                Debug.DrawRay(ray.origin, ray.direction * visionCollider.radius, Color.red);
            }
            else
            {
                animator.SetBool("HasTarget", false);
                Debug.DrawRay(ray.origin, ray.direction * visionCollider.radius, Color.green);
            }
        }

    }

    public void FindTargetsInArea()
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
            }
            i++;
        }

    }
    void InitWayPath()
    {
        WayPointLane[] unitLanes = FindObjectsOfType<WayPointLane>();

        //WayPoint[] waypoints = GameObject.FindObjectsOfType<WayPoint>();
        foreach (var item in unitLanes)
        {
            if (item.WayPointTeam.Value == TeamName.Value)
            {
                UnitLane = item;
                CurrentNode = UnitLane.StartingNode;
                break;
            }
        }
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
            Debug.Log("Cannot find current node");
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
        Debug.Log("I have sighted an Enemy!");
    }
    public void LostEnemySight()
    {
        Debug.Log("I have LOST sight of an Enemy!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindTargetsInArea();
        }

    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        FindTargetsInArea();
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("I sense nothing is near...!");
            VisableTargetList.Remove(other.transform);
        }
    }
}
