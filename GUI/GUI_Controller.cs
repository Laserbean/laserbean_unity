

using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GUI_Controller : MonoBehaviour
{
  
    public GUI_Window_Info ShowInfo;
    public GUI_Window_Info HideInfo; 
    GUI_Window_Info target_window_info; 

    
    [SerializeField] float windowMoveDuration; 
    [SerializeField] float opacityChangeDuration; 



    public bool IsShowing = true; 

    CanvasGroup canvasGroup; 

    RectTransform rectTransform; 

    private void Start() {
        rectTransform = this.GetComponent<RectTransform>(); 

        canvasGroup = this.GetComponent<CanvasGroup>(); 
        if (canvasGroup == null) canvasGroup = this.gameObject.AddComponent<CanvasGroup>(); 

        if (IsShowing) {
            ShowGui();
        } else {
            HideGui(); 
        }
    }

    private void OnValidate() {
        rectTransform = this.GetComponent<RectTransform>(); 

        if (canvasGroup == null) canvasGroup = this.GetComponent<CanvasGroup>(); 
        if (canvasGroup == null) canvasGroup = this.gameObject.AddComponent<CanvasGroup>(); 
    }

    public void ShowGui() {
        // if (IsShowing) return; 
        rectTransform.anchoredPosition = ShowInfo.Position;
        canvasGroup.alpha = ShowInfo.Opacity; 
        IsShowing = true; 
    }

    public void HideGui() {
        // if (!IsShowing) return; 
        rectTransform.anchoredPosition = HideInfo.Position; 
        canvasGroup.alpha = HideInfo.Opacity; 
        IsShowing = false; 
    }



    int lerp_direction = -1; 
    public void ShowGuiLerp() {
        target_window_info = ShowInfo; 
        IsShowing = true; 
        lerp_direction = -1; 
        // timer = windowMoveDuration; 

    }

    public void HideGuiLerp() {
        target_window_info = HideInfo; 
        IsShowing = false; 
        lerp_direction = 1; 
        // timer = 0f; 
    }

    float timer = 0f; 
    private void Update() {
        if ((rectTransform.anchoredPosition.ToVector3() - target_window_info.Position).sqrMagnitude < 0.05f) return; 

        rectTransform.anchoredPosition = Vector3.Lerp(ShowInfo.Position, HideInfo.Position, timer/windowMoveDuration);
        timer += (lerp_direction * Time.deltaTime); 


        canvasGroup.alpha = Mathf.Lerp(ShowInfo.Opacity, HideInfo.Opacity, timer/windowMoveDuration);
        

        
    }
    // IEnumerator ChangeGUI(GUI_Window_Info window_Info) { 
                


    //     yield break; 
    // }
    
}


[System.Serializable]
public struct GUI_Window_Info {
    public Vector3 Position; 

    [Range(0f, 1f)]
    public float Opacity; 
}







#if UNITY_EDITOR 
[CustomEditor(typeof(GUI_Controller))]
public class CustomUIControllerEditor : Editor {


    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GUI_Controller customUIController = (GUI_Controller)target;

        if(GUILayout.Button("ShowGui")) {
            customUIController.ShowGui();             
        }

        if(GUILayout.Button("HideGui")) {
            customUIController.HideGui();             
        }

        if(GUILayout.Button("ShowGuiLerp")) {
            customUIController.ShowGuiLerp();             
        }

        if(GUILayout.Button("HideGuiLerp")) {
            customUIController.HideGuiLerp();             
        }


    }

    public void OnSceneGUI() {
        GUI_Controller customUIController = (GUI_Controller)target;

        RectTransform recttrans = customUIController.transform.GetComponent<RectTransform>(); 
        RectTransform parentrecttrans = customUIController.transform.parent.GetComponent<RectTransform>(); 

        // Debug.Log(parentrecttrans.sizeDelta.x);

        // Debug.Log(recttrans.anchoredPosition);

        // Debug.Log(recttrans.anchoredPosition);
        // Debug.Log("Anchor Min" +recttrans.anchorMin);
        // Debug.Log("Anchor Max" +recttrans.anchorMax);

        Vector3 transformPosition = customUIController.transform.parent.position;

        float scale = customUIController.transform.parent.localScale.x; 


        float xxx = (recttrans.anchorMin.x + recttrans.anchorMax.x) /2;
        float yyy = (recttrans.anchorMin.y + recttrans.anchorMax.y) /2;
        transformPosition.x += Mathf.Lerp(-parentrecttrans.sizeDelta.x/2, parentrecttrans.sizeDelta.x/2, xxx) * scale; 
        transformPosition.y += Mathf.Lerp(-parentrecttrans.sizeDelta.y/2, parentrecttrans.sizeDelta.y/2, yyy) * scale; 



        Handles.color = Color.green;
        Handles.DrawWireCube(transformPosition + (customUIController.ShowInfo.Position * scale), Vector3.one * .5f);

        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(transformPosition + (customUIController.ShowInfo.Position* scale), Quaternion.identity);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(customUIController, "Change Anchor Position");
            customUIController.ShowInfo.Position = (newPosition - transformPosition);
            serializedObject.Update();
        }


        Handles.color = Color.red;
        Handles.DrawWireCube(transformPosition + (customUIController.HideInfo.Position * scale), Vector3.one * .5f);

        EditorGUI.BeginChangeCheck();
        newPosition = Handles.PositionHandle(transformPosition + customUIController.HideInfo.Position* scale, Quaternion.identity);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(customUIController, "Change Anchor Position");
            customUIController.HideInfo.Position = newPosition - transformPosition;
            serializedObject.Update();
        }

        Handles.DrawLine(transformPosition + customUIController.ShowInfo.Position * scale,
        transformPosition + customUIController.HideInfo.Position * scale
        );

    }



}

#endif