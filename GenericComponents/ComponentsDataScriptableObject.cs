
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

namespace Laserbean.General.GenericStuff
{
    public abstract class ComponentsDataScriptableObject : ModuleDataScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        public List<Type> GetAllDependencies()
        {
            return ComponentData.Select(component => (component as ComponentData).ComponentDependency).ToList();
        }

        public override Type GetComponentDataType()
        {
            return typeof(ComponentData);
        }
    }

    public abstract class ComponentsDataScriptableObject<TComponent> : ComponentsDataScriptableObject where TComponent : ComponentData
    {
        public override Type GetComponentDataType()
        {
            return typeof(TComponent);
        }
    }


    [Serializable]
    public abstract class ComponentData : ModuleData
    {
        public Type ComponentDependency { get; protected set; }

        public ComponentData() : base()
        {
            SetComponentDependency();
        }
        protected abstract void SetComponentDependency();
    }

}