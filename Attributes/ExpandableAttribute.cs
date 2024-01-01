using UnityEngine;
using System;
using System.Reflection;



namespace Laserbean.General.EditorAttributes
{
#if UNITY_EDITOR

    using UnityEditor;

#endif

    [AttributeUsage(AttributeTargets.Field)]
    public class ExpandableAttribute : PropertyAttribute
    {
    }

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(ExpandableAttribute))]
    public class ExpandableAttributePropertyDrawer : PropertyDrawer
    {
        Editor m_editor = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);

            if (property.objectReferenceValue == null) return;


            if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label)) {
                EditorGUI.indentLevel++;

                var rect = EditorGUILayout.BeginVertical(GUI.skin.box);
                CustomPropertyExtra.DrawOutlineBox(rect, Color.blue, 1);


                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                if (!m_editor)
                    Editor.CreateCachedEditor(property.objectReferenceValue, null, ref m_editor);
                m_editor.OnInspectorGUI();

                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
            }

        }
    }



    public static class CustomPropertyExtra
    {
        public static void DrawOutlineBox(Rect rect, Color color, int thickness)
        {
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, thickness), color);
            EditorGUI.DrawRect(new Rect(rect.x, rect.yMax - thickness, rect.width, thickness), color);
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, thickness, rect.height), color);
            EditorGUI.DrawRect(new Rect(rect.xMax - thickness, rect.y, thickness, rect.height), color);
        }
    }

#endif

}