using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.StateMachine
{
    [CreateAssetMenu (menuName = "StateMachine/Actions/Aim")]
    public class AimAction : Action
    {
        public override void Act(StateController controller)
        {
            Aim(controller);
        }
        private void Aim(StateController controller)
        {
            controller.navMeshAgent.isStopped = true;
            controller.transform.Rotate(controller.attackTarget.position);
        }
    }
}
