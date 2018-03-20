using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Actions/Attack")]
    public class AttackAction : Action
    {
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
                if (controller.CheckIfCountDownElapsed(controller.unitWeapon.Rate))
                {
                    controller.unitWeapon.FireWeapon();
                }
            }
        }
    }
}

