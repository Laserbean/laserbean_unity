using UnityEngine;
using System;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine.Events;


namespace Laserbean.General.EditorAttributes
{
#if UNITY_EDITOR

    using UnityEditor;

#endif

    [AttributeUsage(AttributeTargets.Field)]
    public class FoldableAttribute : PropertyAttribute
    {
        public Color? color { get => GetColor(); }

        Colour colour;
        public int lineSize { get; private set; }

        public FoldableAttribute()
        {
            this.colour = Colour.None;
            lineSize = 0;
        }

        public FoldableAttribute(Colour colour, int line = 1)
        {
            this.colour = colour;
            lineSize = line;
        }

        Color? GetColor()
        {
            return colour switch {
                Colour.None => null,
                Colour.Red => (Color?)Color.red,
                Colour.Green => (Color?)Color.green,
                Colour.Blue => (Color?)Color.blue,
                Colour.Yellow => (Color?)Color.yellow,
                Colour.Magenta => (Color?)Color.magenta,
                Colour.Cyan => (Color?)Color.cyan,
                Colour.White => (Color?)Color.white,
                Colour.Black => (Color?)Color.black,
                _ => null,
            };
        }

        public enum Colour
        {
            None, Red, Green, Blue, Yellow, Magenta, Cyan, White, Black
        }
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

            FoldableAttribute foldable = attribute as FoldableAttribute;

            // position.y += EditorGUIUtility.standardVerticalSpacing;

            if (foldable.color != null) {
                Rect rect = new(position.x - 0.5f, position.y, position.width + 1f, GetPropertyHeight(property, label));
                CustomPropertyExtra.DrawOutlineBox(rect, Color.blue, foldable.lineSize);
            }

            Rect foldoutpos = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            if (property.isExpanded = EditorGUI.Foldout(foldoutpos, property.isExpanded, label)) {
                EditorGUI.indentLevel++;
                position.y += EditorGUIUtility.singleLineHeight;

                // EditorGUILayout.BeginVertical(GUI.skin.box);

                if (isUnityEvent) {
                    eventDrawer.OnGUI(position, property, label);
                } else {
                    EditorGUI.PropertyField(position, property, label, true);
                }

                // EditorGUILayout.EndVertical();

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


}