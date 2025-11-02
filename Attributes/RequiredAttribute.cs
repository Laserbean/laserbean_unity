using UnityEngine;
#if UNITY_EDITOR
        using UnityEditor;
#endif
public class RequiredAttribute : PropertyAttribute
{
    public enum WarningType { Warning, Error }
    public WarningType Type { get; private set; }

    public RequiredAttribute(WarningType type = WarningType.Error)
    {
        Type = type;
    }
}


#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        RequiredAttribute requiredAttribute = (RequiredAttribute)attribute;

        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
        {
            GUIContent newLabel = new GUIContent("[!] " + label.text);
            Color originalColor = GUI.color;

            if (requiredAttribute.Type == RequiredAttribute.WarningType.Error)
            {
                GUI.color = Color.red;
                // Optionally, pause the editor or prevent play mode
            }
            else if (requiredAttribute.Type == RequiredAttribute.WarningType.Warning)
            {
                GUI.color = Color.yellow;
            }

            EditorGUI.PropertyField(position, property, newLabel);
            GUI.color = originalColor;
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif