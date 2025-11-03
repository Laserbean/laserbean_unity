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

        protected string animBoolName;

        public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
        {
            this.entity = entity;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
            HasExit = false;
        }

        public bool HasExit { get; private set; }

        public virtual void OnEnter()
        {
            HasExit = false;
            StartTime = Time.time;
            entity.Animator?.SetBool(animBoolName, true);
            DoChecks();
        }

        public virtual void OnExit()
        {
            entity.Animator?.SetBool(animBoolName, false);
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
    }

}


