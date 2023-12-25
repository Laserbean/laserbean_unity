using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;


namespace Laserbean.General.GenericStuff
{
    public abstract class ComponentDataBaseScriptableObject : ScriptableObject
    {
        [field: SerializeReference] public List<ComponentDataBase> ComponentData { get; private set; }

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        public virtual void AddData(ComponentDataBase data)
        {
            // if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
            //     return;
            ComponentData.Add(data);
        }

        public virtual Type GetComponentDataType()
        {
            return typeof(ComponentDataBase);
        }

    }

    public abstract class ComponentDataBaseScriptableObject<TComponent> : ComponentDataBaseScriptableObject where TComponent : ComponentDataBase
    {
        public override Type GetComponentDataType()
        {
            return typeof(TComponent);
        }
    }


    [Serializable]
    public abstract class ComponentDataBase
    {
        [SerializeField, HideInInspector] protected string name;
        public void SetComponentName() => name = GetType().Name;

        public ComponentDataBase()
        {
            SetComponentName();
        }
    }
}