using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBrain : MonoBehaviour
{
    Animator anim;
    NavMeshAgent navAgent;
    UnitWeapon unitWeapon;
    UnitHUD unitHUD;
    public List<Transform> VisableTargetList;
    public List<Transform> WayPointList;
    public Transform CurrentTarget;
    public Transform CurrentWayPoint;
    public float TargetDistance;
    public LayerMask TargetMask;
    public float UnitVisionRange;
    public float UnitVisionRadius;
    public StringVariable teamName;
    public string Name { get; set; }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        unitWeapon = GetComponent<UnitWeapon>();
        unitHUD = GetComponent<UnitHUD>();
        FindWayPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetNavAgentTarget(Transform target)
    {
        navAgent.SetDestination(target.position);
    }
    public void FindTargetInVision()
    {
        RaycastHit hit;

        Vector3 p1 = transform.position;

        if (Physics.SphereCast(p1, UnitVisionRadius, transform.forward, out hit, UnitVisionRange))
        {
            CurrentTarget = hit.collider.gameObject.transform;
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
            VisableTargetList.Add(hitColliders[i].GetComponent<Transform>());
            i++;
        }      
    }
    void FindWayPoints()
    {
        WayPoint[] waypoints = GameObject.FindObjectsOfType<WayPoint>();
        foreach (var waypoint in waypoints)
        {
            if (waypoint.WayPointTeam.Value == teamName.Value)
            {
                WayPointList.Add(waypoint.transform);
            }
        }       
    }

}
