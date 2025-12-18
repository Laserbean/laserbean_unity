using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.CoreSystem;

namespace Laserbean.HFiniteStateMachine
{

    public class State
    {
        protected FiniteStateMachine stateMachine;
        protected Entity entity;
        public float StartTime { get; protected set; }
        public float LifeTime
        {
            get
            {
                return Time.time - StartTime;
            }
        }

        public State(Entity entity, FiniteStateMachine stateMachine)
        {
            this.entity = entity;
            this.stateMachine = stateMachine;
            HasExit = false;
        }

        public bool HasExit { get; private set; }

        public virtual void OnEnter()
        {
            HasExit = false;
            StartTime = Time.time;
            DoChecks();
        }

        public virtual void OnExit()
        {
            HasExit = true;
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnFixedUpdate()
        {
            if (HasExit) return;
            DoChecks();
        }

        public virtual void DoChecks()
        {

        }

        public void ChangeState(State state)
        {
            stateMachine.ChangeState(state);
        }
    }

}


