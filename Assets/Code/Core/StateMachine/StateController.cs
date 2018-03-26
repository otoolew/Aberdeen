using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core.StateMachine
{
    public class StateController : MonoBehaviour
    {
        public State currentState;
        public UnityEngine.Transform VisionPoint;
        public State remainState;
        public float UnitVisionRange;
        public float UnitVisionRadius;
        public float UnitScanSpeed;
        public StringVariable teamName;
        public UnitWeaponBehavior unitWeapon;
        public List<UnityEngine.Transform> wayPointList;
        public UnityEngine.Transform chaseTarget;
        public int nextWayPoint;
        public float stateTimeElapsed;
        public LayerMask targetMask;
        public UnityEngine.Transform attackTarget;
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Animator animator;
        private void Start()
        {
            unitWeapon = GetComponent<UnitWeaponBehavior>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            currentState.UpdateState(this);
        }

        void OnDrawGizmos()
        {
            if (currentState != null && VisionPoint != null)
            {
                Gizmos.color = currentState.sceneGizmoColor;
                Gizmos.DrawWireSphere(VisionPoint.position, UnitVisionRadius);
            }
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                currentState = nextState;
                OnExitState();
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return (stateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }
    }
}
