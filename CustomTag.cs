/// Created by Xarbrough on https://answers.unity.com/questions/1470694/multiple-tags-for-one-gameobject.html
/// Edited by laserbean00001

using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

using System.Linq;

#if UNITY_EDITOR
using UnityEditorInternal;
#endif



public class CustomTag : MonoBehaviour 
{
    [SerializeField]
    private List<string> tags = new List<string>();
    
    public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }

    ///<summary>
    ///Checks if this object has any overlapping tags with the _tags provided
    ///</summary>
    public List<string> ContainedTags(List<string> _tags)
    {
        // List<string> res = (List<string>) tags.AsQueryable().Intersect(_tags);
        List<string> res = tags.Intersect(_tags).ToList<string>();
        return res; 
    }
    
    public IEnumerable<string> GetTags()
    {
        return tags;
    }

    public void AddTag(string tag) {
        if (!HasTag(tag)) {
            tags.Add(tag); 
        }
    }
    
    public void Rename(int index, string tagName)
    {
        tags[index] = tagName;
    }
    
    public string GetAtIndex(int index)
    {
        return tags[index];
    }
    
    public int Count
    {
        get { return tags.Count; }
    }

    // public static bool HasTag(GameObject go, string tag) {
    //     CustomTag comp = go.GetComponent<CustomTag>();
    //     if (go != null) {
    //         return comp.HasTag(tag);
    //     }
    //     return go.tag == tag; 
    // }


}

public static class GameObjectExtensions {

    #region CustomTag
    
    public static bool HasTag(this GameObject go, string tag) {
        CustomTag comp = go.GetComponent<CustomTag>();
        if (comp != null) {
            return comp.HasTag(tag);
        }
        return go.tag == tag; 
    }


    ///<summary>
    ///Checks if this object has any overlapping tags with the _tags provided
    ///</summary>
    public static List<string> ContainedTags(this GameObject go, List<string> _tags) {
        CustomTag comp = go.GetComponent<CustomTag>();
        List<string> res = new List<string>();
        if (comp != null) {
            res = comp.GetTags().Intersect(_tags).ToList<string>();
        }
        return res; 
    }


    public static void AddTag(this GameObject go, string tag) {
        CustomTag comp = go.GetComponent<CustomTag>();
        List<string> res = new List<string>();
        if (!comp.HasTag(tag)) {
            comp.AddTag(tag); 
        }
    }
    #endregion


    ///<summary>Does this depending on if it containes the pooled object tag </summary>
    public static void DestoyOrDeactivate(this GameObject go) {
        if (go.HasTag(Constants.TAG_POOLED)) {
            go.SetActive(false); 
        } else {
            GameObject.Destroy(go); 
        }
    }

    // Constants


    public static System.Collections.IEnumerator DoAnimation(this GameObject go, SpriteRenderer spriteRenderer, List<Sprite> sprites, float total_time) {
        int nnnn = sprites.Count;
        for (int j = 0; j < nnnn; j++) {
            spriteRenderer.sprite = sprites[j];
            yield return new WaitForSeconds(total_time/nnnn); 
        }
    }

    
}

# if UNITY_EDITOR

[CustomEditor(typeof(CustomTag))]
public class CustomTagEditor : Editor
{
    private string[] unityTags;
    SerializedProperty tagsProp;
    private ReorderableList list;

    private void OnEnable()
    {
        unityTags = InternalEditorUtility.tags;
        tagsProp = serializedObject.FindProperty("tags");
        list = new ReorderableList(serializedObject, tagsProp, true, true, true, true);
        list.drawHeaderCallback += DrawHeader;
        list.drawElementCallback += DrawElement;
        list.onAddDropdownCallback += OnAddDropdown;
    }

    private void DrawHeader(Rect rect)
    {
        EditorGUI.LabelField(rect, new GUIContent("Tags"), EditorStyles.boldLabel);
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.LabelField(rect, element.stringValue);
    }

    private void OnAddDropdown(Rect buttonRect, ReorderableList list)
    {
        GenericMenu menu = new GenericMenu();

        for (int i = 0; i < unityTags.Length; i++)
        {
            var label = new GUIContent(unityTags[i]);

            // Don't allow duplicate tags to be added.
            if (PropertyContainsString(tagsProp, unityTags[i]))
                menu.AddDisabledItem(label);
            else
                menu.AddItem(label, false, OnAddClickHandler, unityTags[i]);
        }

        menu.ShowAsContext();
    }

    private bool PropertyContainsString(SerializedProperty property, string value)
    {
        if (property.isArray)
        {
            for (int i = 0; i < property.arraySize; i++)
            {
                if (property.GetArrayElementAtIndex(i).stringValue == value)
                    return true;
            }
        }
        else
            return property.stringValue == value;

        return false;
    }

    private void OnAddClickHandler(object tag)
    {
        int index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;

        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        element.stringValue = (string)tag;
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(6);
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        GUILayout.Space(3);
    }
}

#endif