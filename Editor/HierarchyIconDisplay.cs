// Warped Imagination tutorials

using UnityEngine;

using UnityEditor;
using System;
using System.Linq;


[InitializeOnLoad]
public static class HierarchyIconDisplay
{
    static bool _hierarchyHasFocus = false; 
    static EditorWindow _hierarchyEditorWindow; 

    static HierarchyIconDisplay()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;

        EditorApplication.update += OnEditorUpdate;
    }

    private static void OnEditorUpdate()
    {
        if(_hierarchyEditorWindow == null)
            _hierarchyEditorWindow = EditorWindow.GetWindow(System.Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));

        _hierarchyHasFocus = EditorWindow.focusedWindow != null && EditorWindow.focusedWindow == _hierarchyEditorWindow;
    }

    private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {

        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (obj == null) return;

        //NOTE: Ignore prefabs;
        if (PrefabUtility.GetCorrespondingObjectFromOriginalSource(obj) != null) return;

        Component[] components = obj.GetComponents<Component>();

        if (components == null || components.Length == 0) return;


        //NOTE Can add custom priority
        Component component = components.Length > 1 ? components[1] : components[0];

        Type type = component.GetType();

        GUIContent content = EditorGUIUtility.ObjectContent(component, type);
        content.text = null;
        content.tooltip = type.Name;

        if (content.image == null)
            return;


        bool isSelected = Selection.instanceIDs.Contains(instanceID); 
        bool isHovering = selectionRect.Contains(Event.current.mousePosition);
        bool isInfocus = _hierarchyHasFocus;


        Color color = UnityEditorBackgroundColor.Get(isSelected, isHovering, isInfocus);
        Rect backgroundRect = selectionRect;
        backgroundRect.width = 18.5f;

        EditorGUI.DrawRect(backgroundRect, color);

        EditorGUI.LabelField(selectionRect, content);




    }


}

public static class UnityEditorBackgroundColor
{

    static readonly Color k_defaultColor = new(0.7843f, 0.7843f, 0.7843f);
    static readonly Color k_defaultProColor = new(0.2196f, 0.2196f, 0.2196f);

    static readonly Color k_selectedColor = new(0.2274f, 0.447f, 0.6902f);
    static readonly Color k_selectedProColor = new(0.1725f, 0.3647f, 0.5294f);

    static readonly Color k_selectedUnFocusedColor = new(0.68f, 0.68f, 0.68f);
    static readonly Color k_selectedUnFocusedProColor = new(0.3f, 0.3f, 0.3f);


    static readonly Color k_hoveredColor = new(0.698f, 0.698f, 0.698f);
    static readonly Color k_hoveredProColor = new(0.2706f, 0.2706f, 0.2706f);



    public static Color Get(bool isSelected, bool isHovered, bool isWindowFocused)
    {
        if (isSelected) {
            if (isWindowFocused) {
                return EditorGUIUtility.isProSkin ? k_selectedProColor : k_selectedColor;
            }
            else {
                return EditorGUIUtility.isProSkin ? k_selectedUnFocusedProColor : k_selectedUnFocusedColor;
            }
        }
        else if (isHovered) {
            return EditorGUIUtility.isProSkin ? k_hoveredProColor : k_hoveredColor;
        }
        else {
            return EditorGUIUtility.isProSkin ? k_defaultProColor : k_defaultColor;
        }
    }
}
