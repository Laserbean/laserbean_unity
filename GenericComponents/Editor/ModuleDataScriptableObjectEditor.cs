

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

using UnityEditor;
using UnityEditor.Callbacks;

namespace Laserbean.General.GenericStuff
{

    [CustomEditor(typeof(ModuleDataScriptableObject), true)]
    public class ModuleDataScriptableObjectEditor : Editor
    {
        private static List<Type> dataCompTypes = new();

        private ModuleDataScriptableObject dataSO;

        private bool showForceUpdateButtons;
        private bool showAddComponentButtons;

        private void OnEnable()
        {
            dataSO = target as ModuleDataScriptableObject;
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
                var curcomponentdatatype = dataSO.GetComponentDataType();
                foreach (var dataCompType in dataCompTypes) {
                    if (!dataCompType.IsSubclassOf(curcomponentdatatype)) continue;

                    if (GUILayout.Button(dataCompType.Name)) {
                        if (Activator.CreateInstance(dataCompType) is not ModuleData comp)
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

            }
        }


        [DidReloadScripts]
        private static void OnRecompile()
        {
            var componentdatatype = typeof(ModuleData);
            // var componentdatatype = dataSO.GetComponentDataType(); 
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var filteredTypes = types.Where(
                type => type.IsSubclassOf(componentdatatype) && !type.ContainsGenericParameters && type.IsClass
            );
            dataCompTypes = filteredTypes.ToList();
        }
    }
}
