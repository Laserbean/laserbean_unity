using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.CoreSystem;

namespace Laserbean.HFiniteStateMachine
{

    public class StateAnim : State
    {

        protected string animBoolName;

        public StateAnim(StateMachineEntity entity, FiniteStateMachine stateMachine, string animBoolName) : base(entity, stateMachine)
        {
            this.animBoolName = animBoolName;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            entity.Animator?.SetBool(animBoolName, true);
        }

        public override void OnExit()
        {
            base.OnExit();
            entity.Animator?.SetBool(animBoolName, false);
        }

    }

}


