using UnityEngine;
using System;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine.Events;





#if UNITY_EDITOR

using UnityEditor;

#endif

[AttributeUsage(AttributeTargets.Field)]
public class FoldableAttribute : PropertyAttribute
{

}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(FoldableAttribute))]
public class SettingEventPropertyDrawer : PropertyDrawer
{
    Editor m_editor = null;

    private UnityEventDrawer eventDrawer;

    bool isUnityEvent = false;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.type == "UnityEvent") {
            isUnityEvent = true;
        }

        if (eventDrawer == null)
            eventDrawer = new UnityEventDrawer();

        // position.y += EditorGUIUtility.standardVerticalSpacing;

        Rect rect = new(position.x, position.y, position.width, GetPropertyHeight(property, label));
        CustomPropertyExtra.DrawOutlineBox(rect, Color.blue, 1);

        Rect foldoutpos = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        if (property.isExpanded = EditorGUI.Foldout(foldoutpos, property.isExpanded, label)) {
            EditorGUI.indentLevel++;
            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUILayout.BeginVertical(GUI.skin.box);



            if (isUnityEvent) {
                eventDrawer.OnGUI(position, property, label);
            } else {
                EditorGUI.PropertyField(position, property, label, true);
            }

            EditorGUILayout.EndVertical();

            // EditorGUI.LabelField(position, "FISH");


            EditorGUI.indentLevel--;
        }


    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (eventDrawer == null) eventDrawer = new UnityEventDrawer();

        float height = 0;
        // height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        height += EditorGUIUtility.singleLineHeight;

        if (property.isExpanded) {
            if (isUnityEvent) {
                height += eventDrawer.GetPropertyHeight(property, label);
            } else {
                height += EditorGUI.GetPropertyHeight(property, GUIContent.none, true);
            }
            height += EditorGUIUtility.standardVerticalSpacing;

        }
        return height;
    }


    // public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    // {
    //     float height = EditorGUIUtility.singleLineHeight;

    //     if (property.isExpanded)
    //         height += EditorGUI.GetPropertyHeight(property, GUIContent.none, true);

    //     return height;
    // }

}

#endif


