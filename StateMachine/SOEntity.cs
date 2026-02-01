using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.HFiniteStateMachine
{

    /// <summary>
    /// Basic thing that uses the finite state machine. 
    /// </summary>
    public abstract class SOEntity : StateMachineEntity
    {

        protected override void Awake()
        {
        }

        protected override void Update()
        {
            StateMachine.CurrentState?.OnUpdate();
        }

        protected override void FixedUpdate()
        {
            if (StateMachine == null || StateMachine.CurrentState == null || StateMachine.CurrentState.HasExit) return;
            StateMachine.CurrentState?.OnFixedUpdate();
        }
    }

}


