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

        [HideInInspector]
        public Core Core;

        protected virtual void Awake()
        {
            Core = GetComponentInChildren<Core>();

            Animator = GetComponent<Animator>();
            StateMachine = new FiniteStateMachine();
        }

        private void Update()
        {
            Core.LogicUpdate();

            StateMachine.CurrentState?.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (StateMachine == null || StateMachine.CurrentState == null || StateMachine.CurrentState.HasExit) return;
            StateMachine.CurrentState?.OnFixedUpdate();
        }
    }

}


