using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.StateMachine
{
    [System.Serializable]
    public class Transition
    {
        [Tooltip("Arbitary text message")]
        public Decision decision;
        [Tooltip("Arbitary text message")]
        public State trueState;
        [Tooltip("Arbitary text message")]
        public State falseState;
    }
}

