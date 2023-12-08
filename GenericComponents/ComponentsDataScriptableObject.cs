
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

namespace Laserbean.General.GenericStuff
{
    public abstract class ComponentsDataScriptableObject : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        public List<Type> GetAllDependencies()
        {
            return ComponentData.Select(component => component.ComponentDependency).ToList();
        }

        public void AddData(ComponentData data)
        {
            if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
                return;
            ComponentData.Add(data);
        }

    }

    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField, HideInInspector] private string name;

        public Type ComponentDependency { get; protected set; }

        public ComponentData()
        {
            SetComponentName();
            SetComponentDependency();
        }

        public void SetComponentName() => name = GetType().Name;
        protected abstract void SetComponentDependency();
    }

}