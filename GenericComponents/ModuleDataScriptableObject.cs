using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;


namespace Laserbean.General.GenericStuff
{
    public abstract class ModuleDataScriptableObject : ScriptableObject
    {
        [field: SerializeReference] public List<ModuleData> ComponentData { get; private set; }

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        public void AddData(ModuleData data)
        {
            if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
                return;
            ComponentData.Add(data);
        }

        public virtual Type GetComponentDataType()
        {
            return typeof(ModuleData);
        }

    }

    public abstract class ModuleDataScriptableObject<TComponent> : ModuleDataScriptableObject where TComponent : ModuleData
    {
        public override Type GetComponentDataType()
        {
            return typeof(TComponent);
        }
    }


    [Serializable]
    public abstract class ModuleData
    {
        [SerializeField, HideInInspector] protected string name;
        public void SetComponentName() => name = GetType().Name;

        public ModuleData()
        {
            SetComponentName();
        }
    }
}