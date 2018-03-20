using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Decisions/Look")]
    public class LookDecision : Decision
    {

        public override bool Decide(StateController controller)
        {
            bool targetVisible = Look(controller);
            return targetVisible;
        }

        private bool Look(StateController controller)
        {
            RaycastHit hit;

            Debug.DrawRay(controller.VisionPoint.position, controller.VisionPoint.forward.normalized * controller.UnitVisionRange, Color.green);

            if (Physics.SphereCast(controller.VisionPoint.position, controller.UnitVisionRadius, controller.VisionPoint.forward, out hit, controller.UnitVisionRange)
                && hit.collider.CompareTag("Player"))
            {
                controller.chaseTarget = hit.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

