using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.IO;
using Laserbean.General;
using UnityEditorInternal;
using System.Linq;
using System;

public class CustomEnumMethod : Editor
{
    const string extension = ".cs";
    public static void WriteToEnumFile<T>(string path, string name, ICollection<T> data)
    {
        using (StreamWriter file = File.CreateText(path + name + extension))
        {
            file.WriteLine("// CustomEnum Generated Code. Do not edit!");
            file.WriteLine("public enum " + name + " {");

            int i = 0;
            foreach (var line in data)
            {
                string lineRep = line.ToString().Replace(" ", string.Empty);
                if (!string.IsNullOrEmpty(lineRep))
                {
                    file.WriteLine(string.Format("	{0} = {1},", lineRep, i));
                    i++;
                }
            }

            file.WriteLine("}");
        }
        AssetDatabase.ImportAsset(path + name + extension);
    }

    public static List<string> ReadFromEnumFile(string path, string name)
    {
        List<string> list = new(); 
        var thing = File.ReadAllLines(path + name + extension); 

        thing = thing[2..^1]; 
        
        string curstring; 
        int end; 
        foreach(var thin in thing) {
            curstring = thin.TrimStart(); 
            end = curstring.IndexOf("=") - 1; 
            if (end < 0) {
                end = curstring.IndexOf(","); 
            }
            curstring = curstring[..end];
            list.Add(curstring);

            // Debug.Log(curstring); 
        }

        return list; 
    }

    public static void CreateCSFile(string path, string name) {
        if (!SaveAnything.FileExists(path, name, extension)) {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            // FileStream stream = new (path + name + extension, FileMode.Create);
            File.WriteAllText(path + name + extension, ""); 
        }
    }
}

public class CustomEnumAttribute : PropertyAttribute
{
    public string enumName;
    public string asset_path;

    public CustomEnumAttribute(string name, string path)
    {
        enumName = name; 
        asset_path = path; 
    }
}

[Serializable]
public class CustomEnumValueList {
    public List<string> enumList; 
}


[CustomPropertyDrawer(typeof(CustomEnumAttribute))]
public class CustomEnumWriter : PropertyDrawer
{
    string filePath = "Assets/";
    string fileName = "TestMyEnum";

    bool isEditMode = false; 
   

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        var att = attribute as CustomEnumAttribute; 


        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property, label, true);

        var listprop = property.FindPropertyRelative("enumList");

        Rect bottomRect = new (position.x, position.y + GetPropertyHeight(property, label), position.width, lineHeight);       
        bottomRect.y -= lineHeight; 

        Rect buttonrect = bottomRect;

        buttonrect.y -= lineHeight; 
        buttonrect.width = 50; 
        // Rect buttonrect = new (position.x, position.y , 50, lineHeight);

        if (property.isExpanded && GUI.Button(buttonrect, "Create")) {
            CustomEnumMethod.CreateCSFile(att.asset_path, att.enumName);
        }
        
        buttonrect.x += 60; 
        if (!isEditMode) {
            if (property.isExpanded && GUI.Button(buttonrect, "Load")) {
                List<string> new_enum_list = CustomEnumMethod.ReadFromEnumFile(att.asset_path, att.enumName);

                listprop.ClearArray(); 

                for(int i = 0; i < new_enum_list.Count; i++) {
                    listprop.InsertArrayElementAtIndex(i); 
                    listprop.GetArrayElementAtIndex(i).stringValue = new_enum_list[i]; 
                }
                    // Repeat the above steps to add more items if necessary.
                
                isEditMode = true; 
            }

        } else {
            buttonrect.x += 60; 
            if (property.isExpanded && GUI.Button(buttonrect, "Save")) {
                isEditMode = false; 
                SerializedProperty enumNames = property.FindPropertyRelative("enumList");
                List<string> stringlist = new();

                for (int i = 0; i< enumNames.arraySize; i++) {
                    stringlist.Add(enumNames.GetArrayElementAtIndex(i).stringValue);
                }

                CustomEnumMethod.WriteToEnumFile(att.asset_path, att.enumName, stringlist);
            }
            
            buttonrect.x += 60; 
            if (property.isExpanded && GUI.Button(buttonrect, "Cancel")) {
                List<string> new_enum_list = CustomEnumMethod.ReadFromEnumFile(att.asset_path, att.enumName);

                listprop.ClearArray(); 

                for(int i = 0; i < new_enum_list.Count; i++) {
                    listprop.InsertArrayElementAtIndex(i); 
                    listprop.GetArrayElementAtIndex(i).stringValue = new_enum_list[i]; 
                }
                    // Repeat the above steps to add more items if necessary.
                
                isEditMode = true; 
            }

        }


        position = bottomRect;
        EditorGUI.LabelField(position, att.enumName + " editor (" +att.asset_path +")"); 

        EditorGUI.EndProperty();
        
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float totalheight = 0f; 

        var listprop = property.FindPropertyRelative("enumList");

        totalheight += lineHeight * 2; 

        if (property.isExpanded){
            totalheight += lineHeight * 2; 
            if (listprop.isExpanded) {
                totalheight += (lineHeight + EditorGUIUtility.standardVerticalSpacing) * listprop.arraySize; 

                if (listprop.arraySize == 0) {
                    totalheight += 2 * lineHeight; 
                }
            }
            totalheight += lineHeight; 
        }


        return totalheight; 
    }


    // public override void OnInspectorGUI()
    // {
    //     base.OnInspectorGUI();
        
    //     filePath = EditorGUILayout.TextField("Path", filePath);
    //     fileName = EditorGUILayout.TextField("Name", fileName);
    //     if(GUILayout.Button("Save"))
    //     {
    //         EditorMethods.WriteToEnum(filePath, fileName, myScrip.days);
    //     }
    // }
}