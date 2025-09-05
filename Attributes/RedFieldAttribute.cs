using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

public class RedIfEmptyAttribute : PropertyAttribute
{
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(RedIfEmptyAttribute))]
public class RedIfEmptyAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {

        if (string.IsNullOrEmpty(prop.stringValue))
        {
            var col = Color.red;
            Color prev = GUI.color;
            GUI.color = col;
            EditorGUI.PropertyField(position, prop, label, true);
            GUI.color = prev;
        }
        else
        {
            EditorGUI.PropertyField(position, prop, label, true);
        }


        // EditorGUI.LabelField(position, label.text, valueStr);
    }
}
#endif