using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.CoreSystem;

namespace Laserbean.HFiniteStateMachine
{
    public abstract class Entity : MonoBehaviour
    {
        public Animator Animator { get; private set; }
        public FiniteStateMachine StateMachine;

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            StateMachine = new FiniteStateMachine();
        }

        protected virtual void Update()
        {
            StateMachine.CurrentState?.OnUpdate();
        }

        protected virtual void FixedUpdate()
        {
            if (StateMachine == null || StateMachine.CurrentState == null || StateMachine.CurrentState.HasExit) return;
            StateMachine.CurrentState?.OnFixedUpdate();
        }
    }

}


