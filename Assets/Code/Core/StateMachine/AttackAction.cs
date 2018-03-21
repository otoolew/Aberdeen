using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Actions/Attack")]
    public class AttackAction : Action
    {
        Ray ray;
        public override void Act(StateController controller)
        {
            Attack(controller);
        }

        private void Attack(StateController controller)
        {
            RaycastHit hit;

            Debug.DrawRay(controller.VisionPoint.position, controller.VisionPoint.forward.normalized * controller.UnitVisionRange, Color.red);

            if (Physics.SphereCast(controller.VisionPoint.position, controller.UnitVisionRadius, controller.VisionPoint.forward, out hit, controller.UnitVisionRange)
                && hit.collider.CompareTag("Player"))
            {
                RaycastHit rayHit;
                ray.origin = controller.VisionPoint.position;
                ray.direction = controller.VisionPoint.forward;

                if (Physics.Raycast(ray, out rayHit, controller.UnitVisionRange, controller.targetMask))
                {
                    Debug.Log("RayHit " + rayHit.collider.name);
                    controller.AttackTarget = hit.transform;
                    controller.transform.Rotate(hit.transform.position);
                    //controller.navMeshAgent.isStopped = true;
                    if (controller.CheckIfCountDownElapsed(controller.unitWeapon.Rate))
                    {
                        controller.unitWeapon.FireWeapon();
                    }
                }

            }
        }
    }
}

