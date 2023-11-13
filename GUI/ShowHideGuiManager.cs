using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using UnityEngine;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Laserbean.CustomGUI
{

    public interface IGuiManager
    {
        public int GetCurrentState();
        public bool NameExists(string name);
        public int GetStateNumber(string nme);
        public void DoState(int num);
        public void DoStateInstant(int num);
    }

    public class ShowHideGuiManager : MonoBehaviour, IGuiManager
    {
        [HideInInspector]
        public int RowNumber;
        [HideInInspector]
        public int ColumnNumber;

        [SerializeField]
        int cur_state = 0;

        [System.Serializable]
        public class Column
        {
            public string Name = "";
            public GameObject reference_object;
            public List<bool> rows = new();

            public Column(int num)
            {
                rows = new(num);
                for (int i = 0; i < num; i++) {
                    rows.Add(new());
                }
            }

            public int Count {

                get {
                    rows ??= new();
                    return rows.Count;
                }
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateArraysizes();
        }
#endif

        void Start()
        {
            DoStateInstant(cur_state);
        }

        public void UpdateArraysizes()
        {
            if (row_names.Count != RowNumber)
                HelpfulFunctions.Resize(ref row_names, RowNumber);

            if (columns.Count != ColumnNumber)
                HelpfulFunctions.Resize(ref columns, ColumnNumber);

            for (int i = 0; i < columns.Count; i++) {
                if (columns[i] == null) {
                    columns[i] = new Column(RowNumber);
                }
                if (columns[i].Count != RowNumber)
                    HelpfulFunctions.Resize(ref columns[i].rows, RowNumber);
            }

            if (row_names.Count != RowNumber) {
                HelpfulFunctions.Resize(ref row_names, RowNumber);
            }
        }

        [HideInInspector]
        public List<string> row_names = new();
        public List<Column> columns = new();

        [NonSerialized] Dictionary<string, int> name_index_dict = new();
        private void OnEnable()
        {
            name_index_dict.Clear();
            int i = 0;
            foreach (var name in row_names) {
                name_index_dict.Add(name, i++);
            }
        }


        public int GetCurrentState()
        {
            return cur_state;
        }

        public bool NameExists(string name)
        {
            return name_index_dict.Keys.Contains(name);
        }

        public int GetStateNumber(string nme)
        {
            return name_index_dict[nme];
        }

        public void DoState(int num)
        {
            if (row_names.Count < num) throw new IndexOutOfRangeException("Index: " + num + "can't be found in customUIController");

            Debug.Log("Showing " + row_names[num] + " windows");

            foreach (var col in columns) {
                if (col.rows[num]) {
                    col.reference_object?.GetComponent<ShowHideGuiController>()?.ShowGuiLerp();
                }
                else {
                    col.reference_object?.GetComponent<ShowHideGuiController>()?.HideGuiLerp();
                }
            }
            cur_state = num;
        }

        public void DoStateInstant(int num)
        {
            if (row_names.Count < num) throw new IndexOutOfRangeException("Index: " + num + "can't be found in customUIController");

            Debug.Log("Showing " + row_names[num] + " windows");

            foreach (var col in columns) {
                if (col.rows[num]) {
                    col.reference_object?.GetComponent<ShowHideGuiController>()?.ShowGui();
                }
                else {
                    col.reference_object?.GetComponent<ShowHideGuiController>()?.HideGui();
                }
            }
            cur_state = num;
        }
    }


    // public class Column<T>
    // {
    //     public string Name = "ColName";
    //     public T[] rows = new T[RowNumber];

    //     public Column (int num) {
    //         rows = new T[num];
    //     }
    //     public int Count {

    //         get {
    //             rows ??= new T[RowNumber];
    //             return rows.Length; 

    //         }
    //     }
    // }



#if UNITY_EDITOR



    [CustomEditor(typeof(ShowHideGuiManager))]
    public class CustomScriptInscpector : Editor
    {

        ShowHideGuiManager targetScript;

        int prev_col_num;
        int prev_row_num;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // //NOTE This shows the default script thing! 
            // using (new EditorGUI.DisabledScope(true))
            //     EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);

            targetScript = target as ShowHideGuiManager;


            int column_num = targetScript.ColumnNumber;
            int row_num = targetScript.RowNumber;


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Row number: ");
            targetScript.RowNumber = EditorGUILayout.IntField(targetScript.RowNumber);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Column number: ");
            targetScript.ColumnNumber = EditorGUILayout.IntField(targetScript.ColumnNumber);
            EditorGUILayout.EndHorizontal();

            bool editingTextField = EditorGUIUtility.editingTextField;

            if (!editingTextField) {
                targetScript.UpdateArraysizes();
                prev_col_num = column_num;
                prev_row_num = row_num;

            }
            else {
                column_num = prev_col_num;
                row_num = prev_row_num;
            }

            // if (targetScript.columns.Count != targetScript.ColumnNumber) {
            //     return;
            // }




            const float colwidth = 90f;
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField(" Names ", GUILayout.Width(colwidth));
            EditorGUILayout.LabelField(" Reference ", GUILayout.Width(colwidth));

            for (int x = 0; x < row_num; x++) {
                try {
                    targetScript.row_names[x] = EditorGUILayout.TextField(targetScript.row_names[x], GUILayout.Width(colwidth));
                }
                catch {

                }
            }
            EditorGUILayout.EndVertical();
            for (int y = 0; y < column_num; y++) {
                EditorGUILayout.BeginVertical();
                try {
                    targetScript.columns[y].Name = EditorGUILayout.TextField(targetScript.columns[y].Name, GUILayout.Width(colwidth));
                }
                catch {

                    Debug.Log("fish");
                    try {
                        targetScript.columns[y].Name = EditorGUILayout.TextField(targetScript.columns[y].Name, GUILayout.Width(colwidth));
                    }
                    catch {

                    }
                }

                targetScript.columns[y].reference_object = (GameObject)EditorGUILayout.ObjectField("", targetScript.columns[y].reference_object, typeof(GameObject), true, GUILayout.Width(colwidth));
                for (int x = 0; x < row_num; x++) {
                    try {
                        targetScript.columns[y].rows[x] = EditorGUILayout.Toggle(targetScript.columns[y].rows[x]);
                    }
                    catch {
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        return;
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            if (GUI.changed) {
                EditorUtility.SetDirty(targetScript);
                EditorSceneManager.MarkSceneDirty(targetScript.gameObject.scene);
            }

        }
    }


#endif

}