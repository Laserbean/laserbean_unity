using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

namespace Laserbean.General.GenericStuff
{

    public abstract class ComponentGenerator<ThingComponentUser, ThingComponent, ThingSO> : MonoBehaviour
        where ThingComponentUser : MonoBehaviour, IComponentUser
        where ThingComponent : MonoBehaviour, IComponent
        where ThingSO : ComponentsDataScriptableObject
    {

        ThingComponentUser thingUser;

        public ThingSO thingData;



        private void Awake()
        {
            thingUser = this.GetComponent<ThingComponentUser>();
        }


        public event Action OnWeaponGenerating;
        private List<ThingComponent> componentsAlreadyOnObject = new();
        private List<ThingComponent> componentsAddedToObject = new();
        private List<Type> componentDependencies = new();


        [EasyButtons.Button]
        void GenerateThing()
        {
            GenerateThingComponents(thingData);
        }


        [EasyButtons.Button]
        public void ClearStuff()
        {
            thingUser ??= this.GetComponent<ThingComponentUser>();
            OnWeaponGenerating?.Invoke();

            thingUser.SetIsUsable(false);

            componentsAlreadyOnObject.Clear();
            componentsAddedToObject.Clear();
            componentDependencies.Clear();

            componentsAlreadyOnObject = GetComponents<ThingComponent>().ToList();

            var componentsToRemove = componentsAlreadyOnObject;

            foreach (var weaponComponent in componentsToRemove) {
                weaponComponent.BeforeDestroy();

                if (Application.isPlaying) {
                    Destroy(weaponComponent);
                } else {
                    DestroyImmediate(weaponComponent);
                }
            }
        }


        public void GenerateThingComponents(ThingSO data)
        {
            thingUser ??= this.GetComponent<ThingComponentUser>();
            OnWeaponGenerating?.Invoke();

            // thingUser.SetComponentData(data);

            if (data is null) {
                thingUser.SetIsUsable(false);
                return;
            }

            componentsAlreadyOnObject.Clear();
            componentsAddedToObject.Clear();
            componentDependencies.Clear();

            componentsAlreadyOnObject = GetComponents<ThingComponent>().ToList();

            componentDependencies = data.GetAllDependencies();

            foreach (var dependency in componentDependencies) {
                if (componentsAddedToObject.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                var weaponComponent =
                    componentsAlreadyOnObject.FirstOrDefault(component => component.GetType() == dependency) ?? gameObject.AddComponent(dependency) as ThingComponent;
                weaponComponent.Init();

                componentsAddedToObject.Add(weaponComponent);
            }

            var componentsToRemove = componentsAlreadyOnObject.Except(componentsAddedToObject);

            foreach (var weaponComponent in componentsToRemove) {
                Destroy(weaponComponent);
            }

            thingUser.SetIsUsable(true);
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
