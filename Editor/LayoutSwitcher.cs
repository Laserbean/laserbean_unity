using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditorInternal; 
using System.IO; 

using System.Linq;
using System.Reflection;
using UnityEditor.ShortcutManagement;


public static class LayoutSwitcherTool
{   
    [Shortcut("LayoutSwitcher/Layout1", KeyCode.Alpha1, ShortcutModifiers.Alt)]
    public static void Layout1MenuItem() {
        OpenLayout("Default"); 
    }

    [Shortcut("LayoutSwitcher/Layout2", KeyCode.Alpha2, ShortcutModifiers.Alt)]
    public static void Layout2MenuItem() {
        OpenLayout("Animation"); 
    }

    [Shortcut("LayoutSwitcher/Layout3", KeyCode.Alpha3, ShortcutModifiers.Alt)]
    public static void Layout3MenuItem() {
        OpenLayout("Profile"); 
    }

    [Shortcut("LayoutSwitcher/Layout4", KeyCode.Alpha4, ShortcutModifiers.Alt)]
    public static void Layout4MenuItem() {
        OpenLayout("Layout4"); 
    }


    static bool OpenLayout(string name) {
        string path = GetWindowLayoutPath(name); 
        if (string.IsNullOrEmpty(path)) return false; 

        System.Type windowLayoutType = typeof(Editor).Assembly.GetType("UnityEditor.WindowLayout");

        if (windowLayoutType != null) {
            MethodInfo tryLoadWindowLayoutMethod = windowLayoutType.GetMethod("LoadWindowLayout",
                BindingFlags.Public | BindingFlags.Static,
                null,
                new System.Type[] {typeof(string), typeof(bool)},
                null); 
            if (tryLoadWindowLayoutMethod != null) {
                object[] arguments = new object[] {path, false}; 
                bool result = (bool)tryLoadWindowLayoutMethod.Invoke(null, arguments); 
                return result; 
            }
        }
        return false; 
    }


    static string GetWindowLayoutPath(string name) {
        string layoutsPreferencesPath = Path.Combine(InternalEditorUtility.unityPreferencesFolder, "Layouts");
        string layoutsModePreferencesPath = Path.Combine(layoutsPreferencesPath, ModeService.currentId); 


        if (Directory.Exists(layoutsModePreferencesPath)) {
            string[] layoutPaths = Directory.GetFiles(layoutsModePreferencesPath).Where(path => path.EndsWith(".wlt")).ToArray();
            if (layoutPaths != null) {
                foreach(var layoutPath in layoutPaths) {
                    if (string.Compare(name, Path.GetFileNameWithoutExtension(layoutPath)) == 0) return layoutPath; 
                }
            }

        }

        return null; 
    }


}
