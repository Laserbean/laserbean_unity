using UnityEngine;
using UnityEditor;

/// <summary>
/// Use this attribute in combination with a [SerializeField] attribute on top of a property to display the property name. Example:
/// [field: SerializeField, UsePropertyName]
/// public int number { get; private set; }
/// </summary>
public class UsePropertyNameAttribute : PropertyAttribute
{
    
}


[CustomPropertyDrawer(typeof(UsePropertyNameAttribute))]
public class UsePropertyNameAttributeDrawer : PropertyDrawer
{
    // TODO Make useful for array properties
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.name.EndsWith("k__BackingField"))
        {
            FixLabel(label);
        }

        EditorGUI.PropertyField(position, property, label, true);
    }

    private static void FixLabel(GUIContent label)
    {
        var text = label.text;
        var firstLetter = char.ToUpper(text[1]);
        label.text = firstLetter + text.Substring(2, text.Length - 19);
    }
}