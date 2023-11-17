

using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Laserbean.CustomGUI
{
public class MultistateGuiController : MonoBehaviour
{
    public List<GUI_Window_Info> window_info_list = new(); 

    int cur_window_ind = 0;
    int target_window_ind = 0;


    Vector3 target_window_position = Vector3.zero; 

    [Min(0.01f)]
    [SerializeField] float windowMoveDuration = 0.01f; 
    // [SerializeField] float opacityChangeDuration; 


    CanvasGroup canvasGroup; 

    RectTransform rectTransform; 
    private void Awake() {
        rectTransform = this.GetComponent<RectTransform>(); 
        canvasGroup = this.GetComponent<CanvasGroup>(); 
        if (canvasGroup == null) canvasGroup = this.gameObject.AddComponent<CanvasGroup>(); 
        
    }


    private void OnValidate() {
        rectTransform = this.GetComponent<RectTransform>(); 

        if (canvasGroup == null) canvasGroup = this.GetComponent<CanvasGroup>(); 
        if (canvasGroup == null) canvasGroup = this.gameObject.AddComponent<CanvasGroup>(); 
    }


    public void ShowGuiAt(int number) {
        var targetWindowInfo = window_info_list[number];

        cur_window_ind = target_window_ind; 
        target_window_ind = number; 



        rectTransform.anchoredPosition = targetWindowInfo.Position;
        canvasGroup.alpha = targetWindowInfo.Opacity; 
        canvasGroup.interactable = targetWindowInfo.Interactable; 
        canvasGroup.blocksRaycasts = targetWindowInfo.BlocksRaycast; 


        target_window_position = targetWindowInfo.Position; 
        timer = windowMoveDuration;
    }




    public void StartGuiLerpAt(int number) {
        var targetWindowInfo = window_info_list[number];
        cur_window_ind = target_window_ind; 
        target_window_ind = number; 

        canvasGroup.interactable = targetWindowInfo.Interactable; 
        canvasGroup.blocksRaycasts = targetWindowInfo.BlocksRaycast; 

        target_window_position = targetWindowInfo.Position; 
        timer = 0f; 
    }



    float timer = 0f; 
    private void Update() {

        // if (target_window_position == Vector3.zero) return; 
        // if ((rectTransform.anchoredPosition.ToVector3() - target_window_position).sqrMagnitude < 0.05f) {
        //     cur_window_ind = target_window_ind; 
        //     return; 
        // }

        // Debug.Log(cur_window_ind + " " + target_window_ind); 

        if (timer > windowMoveDuration) timer = windowMoveDuration;
        // if (timer < 0f) timer = 0;

        rectTransform ??= this.GetComponent<RectTransform>(); 


        rectTransform.anchoredPosition = Vector3.Lerp(window_info_list[cur_window_ind].Position, window_info_list[target_window_ind].Position, timer/windowMoveDuration);
        timer += Time.unscaledDeltaTime; 


        canvasGroup.alpha = Mathf.Lerp(window_info_list[cur_window_ind].Opacity, window_info_list[target_window_ind].Opacity, timer/windowMoveDuration);
        

        
    }

}



#if UNITY_EDITOR 
[CustomEditor(typeof(MultistateGuiController))]
public class MultistateGuiControllerEditor : Editor {

    int number_to_move = 0;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        MultistateGuiController customUIController = (MultistateGuiController)target;

        number_to_move = EditorGUILayout.IntField("Move To:", number_to_move);

        if(GUILayout.Button("ShowGui")) {
            customUIController.ShowGuiAt(number_to_move);             
        }

        if(GUILayout.Button("ShowGuiLerp")) {
            customUIController.StartGuiLerpAt(number_to_move);             
        }



    }

    const float handlesize = 0.1f; 
    public void OnSceneGUI() {
        float zoom = SceneView.currentDrawingSceneView.camera.orthographicSize;

        MultistateGuiController customUIController = (MultistateGuiController)target;

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


        // foreach(var ShowInfo in customUIController.window_info_list) {
        for(int i = 0; i < customUIController.window_info_list.Count; i++ ) {
            var ShowInfo = customUIController.window_info_list[i];
            Handles.color = Color.green;
            Handles.DrawWireCube(transformPosition + (ShowInfo.Position * scale), Vector3.one * handlesize * zoom);

            EditorGUI.BeginChangeCheck();
            Vector3 newPosition = Handles.PositionHandle(transformPosition + (ShowInfo.Position* scale), Quaternion.identity);
            if (EditorGUI.EndChangeCheck()) {
                Undo.RecordObject(customUIController, "Change Anchor Position");
                customUIController.window_info_list[i].Position = (newPosition - transformPosition) / scale;
                serializedObject.Update();
            }

        }


        // Handles.color = Color.red;
        // Handles.DrawWireCube(transformPosition + (customUIController.HideInfo.Position * scale), Vector3.one * .5f);

        // EditorGUI.BeginChangeCheck();
        // newPosition = Handles.PositionHandle(transformPosition + customUIController.HideInfo.Position* scale, Quaternion.identity);
        // if (EditorGUI.EndChangeCheck()) {
        //     Undo.RecordObject(customUIController, "Change Anchor Position");
        //     customUIController.HideInfo.Position = newPosition - transformPosition;
        //     serializedObject.Update();
        // }

        // // Handles.DrawLine(transformPosition + customUIController.ShowInfo.Position * scale,
        // // transformPosition + customUIController.HideInfo.Position * scale
        // // );

    }



}

#endif

}