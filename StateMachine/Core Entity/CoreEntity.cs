using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.CoreSystem;

namespace Laserbean.HFiniteStateMachine
{

    public abstract class CoreEntity : Entity
    {


        [HideInInspector]
        public Core Core;

        protected override void Awake()
        {
            base.Awake(); 
            Core = GetComponentInChildren<Core>();

        }

        protected override void Update()
        {
            base.Update(); 
            Core.LogicUpdate();
        }
    }

}


