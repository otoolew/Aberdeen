using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Core.StateMachine
{
    public class StateController : MonoBehaviour
    {
        public State currentState;
        public Transform VisionPoint;
        public State remainState;
        public float UnitVisionRange;
        public float UnitVisionRadius;
        public StringVariable teamName;
        public UnitWeaponBehavior unitWeapon;
        public List<Transform> wayPointList;
        public Transform chaseTarget;
        public int nextWayPoint;
        public float stateTimeElapsed;
        public LayerMask targetMask;
        public Transform AttackTarget;
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
