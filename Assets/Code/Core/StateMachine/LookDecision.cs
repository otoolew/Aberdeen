using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Decisions/Look")]
    public class LookDecision : Decision
    {
        Ray ray;
        public override bool Decide(StateController controller)
        {
            bool targetVisible = Look(controller);
            return targetVisible;
        }
        /// <summary>
        /// Use Tags HERE!!!
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        private bool Look(StateController controller)
        {
            RaycastHit hit;

            Debug.DrawRay(controller.VisionPoint.position, controller.VisionPoint.forward.normalized * controller.UnitVisionRange, Color.green);

            if (Physics.SphereCast(controller.VisionPoint.position, controller.UnitVisionRadius, controller.VisionPoint.forward, out hit, controller.UnitVisionRange, controller.targetMask))
            {
                RaycastHit rayHit;    
                ray.origin = controller.VisionPoint.position;
                ray.direction = controller.VisionPoint.forward;

                if (Physics.Raycast(ray, out rayHit, controller.UnitVisionRange, controller.targetMask))
                {
                    Debug.Log("RayHit " + rayHit.collider.name);
                    controller.chaseTarget = hit.transform;
                    controller.AttackTarget = hit.transform;
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}

