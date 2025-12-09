using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.CoreSystem;

namespace Laserbean.HFiniteStateMachine
{

    public class CoreState
    {
        protected FiniteStateMachine stateMachine;
        protected CoreEntity entity;
        protected Core core;

        public float startTime { get; protected set; }

        protected string animBoolName;

        public CoreState(CoreEntity entity, FiniteStateMachine stateMachine, string animBoolName)
        {
            this.entity = entity;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
            core = entity.Core;
            HasExit = false;
        }

        public bool HasExit { get; private set; }

        public virtual void OnEnter()
        {
            HasExit = false;
            startTime = Time.time;
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


