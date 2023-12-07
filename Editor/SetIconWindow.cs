// From warped imaginations
// edited by Laserbean;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

using System;
using System.IO;
using Laserbean.General;

public class SetIconWindow : EditorWindow
{
    const string k_menuPath = "Assets/ScriptIcon/Set Script Icon..";
    const string k_menuPath2 = "Assets/ScriptIcon/Edit Icon..";


    const string k_ImageEditorPath = "D:/Program files/Paint.net/PaintDotNet.exe";
    const string k_DefaultIconPath = "Assets/Dev/ScriptIcons";
    const string k_DefaultIconName = "NewScriptIcon";

    const string k_ScriptIconLabel = "ScriptIcon";

    const int k_DefaultIconSize = 64;

    List<Texture2D> m_icons = null;
    int m_selectedIcon = 0;

    [MenuItem(k_menuPath, priority = 0)]
    private static void ShowWindow()
    {
        var window = GetWindow<SetIconWindow>();
        window.titleContent = new GUIContent("Set Icon");
        window.Show();
    }

    [MenuItem(k_menuPath, validate = true)]
    private static bool ShowMenuItemValidation()
    {
        foreach (UnityEngine.Object asset in Selection.objects) {
            if (asset.GetType() != typeof(MonoScript)) return false;
        }
        return true;
    }


    [MenuItem(k_menuPath2, priority = 1)]
    private static void OpenWithPaintNet()
    {
        var filepath = AssetDatabase.GetAssetPath(Selection.objects[0]);
        // Debug.Log(filepath) ;
        OpenExternalApplication.TryOpen(k_ImageEditorPath, filepath);
    }

    [MenuItem(k_menuPath2, validate = true)]
    private static bool OpenWithPaintNetValidation()
    {
        if (Selection.objects.Length > 1) return false;
        foreach (UnityEngine.Object asset in Selection.objects) {
            if (asset.GetType() != typeof(Texture2D)) return false;
        }
        return true;
    }


    private void OnGUI()
    {
        if (m_icons == null) {
            m_icons = LoadIcons($"t:texture2d l:{k_ScriptIconLabel}");
        }

        if (GUILayout.Button("Create Icon", GUILayout.Width(100))) {
            CreateIconAndOpen();
        }

        if (m_icons == null) {
            GUILayout.Label($"No Icons to Display. Set the label of the desired icons to \"{k_ScriptIconLabel}\"");
            if (GUILayout.Button("Close", GUILayout.Width(100))) {
                Close();
            }

        } else {
            m_selectedIcon = GUILayout.SelectionGrid(m_selectedIcon, m_icons.ToArray(), 5);

            if (Event.current == null) return;

            if (Event.current.isKey) {
                switch (Event.current.keyCode) {
                    case KeyCode.KeypadEnter:
                    case KeyCode.Return:
                        ApplyIcon(m_icons[m_selectedIcon]);
                        Close();
                        break;
                    case KeyCode.Escape:
                        Close();
                        break;
                    default:
                        break;
                }
            } else {
                if (Event.current.button == 0 && Event.current.clickCount == 2) {
                    ApplyIcon(m_icons[m_selectedIcon]);
                    Close();
                }
            }
            if (GUILayout.Button("Apply", GUILayout.Width(100))) {
                ApplyIcon(m_icons[m_selectedIcon]);
                Close();
            }

        }

    }


    private void CreateIconAndOpen()
    {
        if (!AssetDatabase.IsValidFolder(k_DefaultIconPath)) {
            System.IO.Directory.CreateDirectory(k_DefaultIconPath);
            AssetDatabase.Refresh();
        }
        var filepath = SaveAnything.GetUniqueFileNameFullPath(k_DefaultIconPath, k_DefaultIconName, "png");

        Texture2D texture = new Texture2D(k_DefaultIconSize, k_DefaultIconSize);  // Adjust the size as needed
        byte[] pngData = texture.EncodeToPNG();

        File.WriteAllBytes(filepath, pngData);
        AssetDatabase.Refresh();
        SetLabelOnAsset(filepath, k_ScriptIconLabel);


        OpenExternalApplication.TryOpen(k_ImageEditorPath, filepath);
    }

    private void SetLabelOnAsset(string assetPath, string label)
    {
        var obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
        if (obj == null) return;

        AssetDatabase.SetLabels(obj, new string[] { label });
        AssetDatabase.ImportAsset(assetPath);
    }


    void ApplyIcon(Texture2D icon)
    {
        AssetDatabase.StartAssetEditing();
        foreach (UnityEngine.Object asset in Selection.objects) {
            string path = AssetDatabase.GetAssetPath(asset);
            MonoImporter monoImporter = AssetImporter.GetAtPath(path) as MonoImporter;
            monoImporter.SetIcon(icon);
            AssetDatabase.ImportAsset(path);
        }
        AssetDatabase.StopAssetEditing();
        AssetDatabase.Refresh();
    }



    List<Texture2D> LoadIcons(string assetSearch)
    {
        var m_icons = new List<Texture2D>();
        string[] assetGuids = AssetDatabase.FindAssets(assetSearch);

        if (assetGuids.Length == 0) {
            m_icons = null;
        } else {
            foreach (string assetGuid in assetGuids) {
                string path = AssetDatabase.GUIDToAssetPath(assetGuid);
                m_icons.Add(AssetDatabase.LoadAssetAtPath<Texture2D>(path));
            }
        }

        return m_icons;
    }

}