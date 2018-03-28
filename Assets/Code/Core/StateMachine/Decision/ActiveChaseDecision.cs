using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Decisions/ActiveChaseDecision")]
    public class ActiveChaseDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;
            return chaseTargetIsActive;
        }
    }
}

