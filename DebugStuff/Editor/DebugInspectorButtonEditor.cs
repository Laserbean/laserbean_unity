using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;



[CustomEditor(typeof(DebugInspectorButton))]
public class DebugInspectorButtonEditor : Editor
{

    public override void OnInspectorGUI()
    {
        var thistarget = target as DebugInspectorButton;
        base.OnInspectorGUI();

        foreach (var unityeventthing in thistarget.Events) {
            if (string.IsNullOrEmpty(unityeventthing.Name)) continue;
            if (GUILayout.Button(unityeventthing.Name)) {
                unityeventthing.unityEvent.Invoke();
            }
        }
    }
}
