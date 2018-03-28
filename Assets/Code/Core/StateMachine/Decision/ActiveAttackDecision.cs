using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/Decisions/ActiveAttackDecision")]
    public class ActiveAttackDecision : Decision
    {
        /// <summary>
        /// Decision is made based on Current Target
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public override bool Decide(StateController controller)
        {
            bool result = false;
            if(controller.attackTarget != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}