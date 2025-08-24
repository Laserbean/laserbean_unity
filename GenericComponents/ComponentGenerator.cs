using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

namespace Laserbean.General.GenericStuff
{

    public abstract class ComponentGenerator<ComponentUser, Component, ComponentDataObject> : MonoBehaviour
        where ComponentUser : MonoBehaviour, IComponentUser
        where Component : MonoBehaviour, IComponent
        where ComponentDataObject : ComponentsDataScriptableObject
    {

        ComponentUser componentUser;

        public ComponentDataObject componentDataObject;
        private void Awake()
        {
            componentUser = GetComponent<ComponentUser>();
        }


        public event Action OnComponentGenerating;
        private List<Component> componentsAlreadyOnObject = new();
        private List<Component> componentsAddedToObject = new();
        private List<Type> componentDependencies = new();


        [EasyButtons.Button] public void Generate_Components()
        {
            GenerateComponents(componentDataObject);
        }


        [EasyButtons.Button]
        public void ClearAddedComponents()
        {
            componentUser ??= GetComponent<ComponentUser>();
            OnComponentGenerating?.Invoke();

            componentUser.SetIsUsable(false);

            componentsAlreadyOnObject.Clear();
            componentsAddedToObject.Clear();
            componentDependencies.Clear();

            componentsAlreadyOnObject = GetComponents<Component>().ToList();

            var componentsToRemove = componentsAlreadyOnObject;

            foreach (var cur_component in componentsToRemove) {
                cur_component.BeforeDestroy();

                if (Application.isPlaying) {
                    Destroy(cur_component);
                } else {
                    DestroyImmediate(cur_component);
                }
            }
        }


        public void GenerateComponents(ComponentDataObject data)
        {
            componentUser ??= GetComponent<ComponentUser>();
            OnComponentGenerating?.Invoke();

            // thingUser.SetComponentData(data);

            if (data is null) {
                componentUser.SetIsUsable(false);
                return;
            }

            componentsAlreadyOnObject.Clear();
            componentsAddedToObject.Clear();
            componentDependencies.Clear();

            componentsAlreadyOnObject = GetComponents<Component>().ToList();

            componentDependencies = data.GetAllDependencies();

            foreach (var dependency in componentDependencies) {
                if (componentsAddedToObject.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                var current_component = componentsAlreadyOnObject.FirstOrDefault(component => component.GetType() == dependency) ?? gameObject.AddComponent(dependency) as Component;
                current_component.Init();

                componentsAddedToObject.Add(current_component);
            }

            var componentsToRemove = componentsAlreadyOnObject.Except(componentsAddedToObject);

            foreach (var cur_component in componentsToRemove) {
                Destroy(cur_component);
            }

            componentUser.SetIsUsable(true);
        }
    }


    public interface IComponent
    {
        public void Init();
        public void BeforeDestroy(); 

    }

    public interface IComponentUser
    {
        public void GenerateComponents(); 


        void SetIsUsable(bool val);
        void SetComponentData(ComponentsDataScriptableObject data);
    }
}
