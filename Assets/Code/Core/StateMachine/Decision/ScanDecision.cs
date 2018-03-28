using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Decisions/Scan")]
    public class ScanDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool noEnemyInSight = Scan(controller);
            return noEnemyInSight;
        }

        private bool Scan(StateController controller)
        {
            controller.navMeshAgent.isStopped = true;
            controller.VisionPoint.Rotate(0, controller.UnitScanSpeed * Time.deltaTime, 0);
            return controller.CheckIfCountDownElapsed(2);
        }
    }
}

