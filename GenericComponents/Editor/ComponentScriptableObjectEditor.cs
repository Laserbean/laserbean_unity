

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

using UnityEditor;
using UnityEditor.Callbacks;

namespace Laserbean.General.GenericStuff
{


    [CustomEditor(typeof(ComponentsDataScriptableObject), true)]
    public class ComponentScriptableObjectEditor : Editor
    {
        private static List<Type> dataCompTypes = new();

        private ComponentsDataScriptableObject dataSO;

        private bool showForceUpdateButtons;
        private bool showAddComponentButtons;

        private void OnEnable()
        {
            dataSO = target as ComponentsDataScriptableObject;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // if (GUILayout.Button("Set Number of Attacks")) {
            //     // foreach (var item in dataSO.ComponentData) {
            //     //     item.InitializeAttackData(dataSO.NumberOfAttacks);
            //     // }
            // }

            showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");

            if (showAddComponentButtons) {
                foreach (var dataCompType in dataCompTypes) {
                    if (GUILayout.Button(dataCompType.Name)) {
                        if (Activator.CreateInstance(dataCompType) is not ComponentData comp)
                            return;

                        // comp.InitializeAttackData(dataSO.NumberOfAttacks);

                        dataSO.AddData(comp);

                        EditorUtility.SetDirty(dataSO);
                    }
                }
            }

            showForceUpdateButtons = EditorGUILayout.Foldout(showForceUpdateButtons, "Force Update Buttons");

            if (showForceUpdateButtons) {
                if (GUILayout.Button("Force Update Component Names")) {
                    foreach (var item in dataSO.ComponentData) {
                        item.SetComponentName();
                    }
                }

                // if (GUILayout.Button("Force Update Attack Names")) {
                //     foreach (var item in dataSO.ComponentData) {
                //         item.SetAttackDataNames();
                //     }
                // }
            }
        }


        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var filteredTypes = types.Where(
                type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );
            dataCompTypes = filteredTypes.ToList();
        }
    }
}
