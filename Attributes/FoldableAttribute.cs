using UnityEngine;
using System;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine.Events;


namespace Laserbean.General.EditorAttributes
{
#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine.Profiling;

#endif

    [AttributeUsage(AttributeTargets.Field)]
    public class FoldableAttribute : PropertyAttribute
    {
        public Color? color { get => GetColor(); }

        Colour colour;
        public int lineSize { get; private set; }

        internal FoldableInstance Instance = new();

        public string ID { get; private set; }


        public FoldableAttribute()
        {
            this.colour = Colour.None;
            lineSize = 0;
            ID = RandomStatic.GenerateRandomString(5);
        }

        public FoldableAttribute(Colour colour, int line = 1)
        {
            this.colour = colour;
            lineSize = line;

            ID = RandomStatic.GenerateRandomString(5);
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

    internal class FoldableInstance
    {
        public bool IsUnityEvent = false;

        UnityEventDrawer eventDrawer = null;

        public UnityEventDrawer EventDrawer {
            get {
                if (eventDrawer == null) {
                    eventDrawer = new UnityEventDrawer();
                }
                return eventDrawer;
            }
        }

        public float lastUpdateTime = 0f;
        public bool isExpanded = false;
    }



#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(FoldableAttribute))]
    public class SettingEventPropertyDrawer : PropertyDrawer
    {
        Editor m_editor = null;

        private const float updateDelay = 0.1f;  // Set your desired delay here

        FoldableAttribute foldable;
        public void OnEnable()
        {
            foldable = attribute as FoldableAttribute;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            foldable ??= attribute as FoldableAttribute;

            if (foldable == null) return;

            foldable.Instance.IsUnityEvent = property.type.StartsWith("UnityEvent");


            EditorGUI.BeginChangeCheck();

            Rect boundingRect = new(position.x - 0.5f, position.y, position.width + 1f, GetPropertyHeight(property, label));
            Rect foldoutpos = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            if (foldable.Instance.isExpanded = EditorGUI.Foldout(foldoutpos, foldable.Instance.isExpanded, label)) {
                EditorGUI.indentLevel++;
                position.y += EditorGUIUtility.singleLineHeight;

                if (foldable.Instance.IsUnityEvent) {
                    foldable.Instance.EventDrawer.OnGUI(position, property, label);
                } else {
                    EditorGUI.PropertyField(position, property, label, true);
                }

                EditorGUI.indentLevel--;
            }

            if (EditorGUI.EndChangeCheck()) {
                foldable.Instance.lastUpdateTime = Time.realtimeSinceStartup + updateDelay;
            }

            if (Time.realtimeSinceStartup > foldable.Instance.lastUpdateTime && foldable.Instance.isExpanded) {
            // if (foldable.Instance.isExpanded) {
                if (foldable.color != null)
                    CustomPropertyExtra.DrawOutlineBox(boundingRect, (Color)foldable.color, foldable.lineSize);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            foldable ??= attribute as FoldableAttribute;

            float height = 0;
            // height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            height += EditorGUIUtility.singleLineHeight;

            if (foldable.Instance.isExpanded) {
                if (foldable.Instance.IsUnityEvent) {
                    height += foldable.Instance.EventDrawer.GetPropertyHeight(property, label);
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