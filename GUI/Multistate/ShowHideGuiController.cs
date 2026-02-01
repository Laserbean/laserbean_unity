

// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Laserbean.General;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.PlayerLoop;

// namespace Laserbean.CustomGUI
// {
//     [Obsolete("use the Multistate GUI stuff")]
//     public class ShowHideGuiController : MonoBehaviour
//     {
//         public GUI_Window_Info ShowInfo;
//         public GUI_Window_Info HideInfo;
//         Vector3 target_window_position = Vector3.zero;


//         [SerializeField] float windowMoveDuration;
//         // [SerializeField] float opacityChangeDuration; 



//         public bool IsShowing = true;

//         CanvasGroup canvasGroup;

//         RectTransform rectTransform;

//         private void Awake()
//         {
//             rectTransform = this.GetComponent<RectTransform>();
//             canvasGroup = this.GetComponent<CanvasGroup>();

//         }

//         private void Start()
//         {

//             if (canvasGroup == null) canvasGroup = this.gameObject.AddComponent<CanvasGroup>();

//             if (IsShowing) {
//                 ShowGui();
//             }
//             else {
//                 HideGui();
//             }
//         }

//         private void OnValidate()
//         {
//             rectTransform = this.GetComponent<RectTransform>();

//             if (canvasGroup == null) canvasGroup = this.GetComponent<CanvasGroup>();
//             if (canvasGroup == null) canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
//         }

//         public void ShowGui()
//         {
//             // if (IsShowing) return; 
//             rectTransform.anchoredPosition = ShowInfo.Position;
//             canvasGroup.alpha = ShowInfo.Opacity;
//             canvasGroup.interactable = ShowInfo.Interactable;
//             canvasGroup.blocksRaycasts = ShowInfo.BlocksRaycast;


//             IsShowing = true;
//             ShowGuiLerp();
//             timer = 0f;

//         }

//         public void HideGui()
//         {
//             // if (!IsShowing) return; 
//             rectTransform.anchoredPosition = HideInfo.Position;
//             canvasGroup.alpha = HideInfo.Opacity;
//             canvasGroup.interactable = HideInfo.Interactable;
//             canvasGroup.blocksRaycasts = HideInfo.BlocksRaycast;
            


//             IsShowing = false;
//             HideGuiLerp();
//             timer = windowMoveDuration;


//         }



//         int lerp_direction = -1;
//         public void ShowGuiLerp()
//         {
//             target_window_position = ShowInfo.Position;
//             canvasGroup.interactable = ShowInfo.Interactable;
//             canvasGroup.blocksRaycasts = ShowInfo.BlocksRaycast;

//             IsShowing = true;
//             lerp_direction = -1;
//             // timer = windowMoveDuration; 

//         }

//         public void HideGuiLerp()
//         {
//             target_window_position = HideInfo.Position;
//             canvasGroup.interactable = HideInfo.Interactable;
//             canvasGroup.blocksRaycasts = HideInfo.BlocksRaycast;


//             IsShowing = false;
//             lerp_direction = 1;
//             // timer = 0f; 
//         }

//         float timer = 0f;
//         private void Update()
//         {
//             if (target_window_position == Vector3.zero) return;
//             if ((rectTransform.anchoredPosition.ToVector3() - target_window_position).sqrMagnitude < 0.05f) return;

//             if (timer > windowMoveDuration) timer = windowMoveDuration;
//             if (timer < 0f) timer = 0;


//             rectTransform.anchoredPosition = Vector3.Lerp(ShowInfo.Position, HideInfo.Position, timer / windowMoveDuration);
//             timer += (lerp_direction * Time.unscaledDeltaTime);


//             canvasGroup.alpha = Mathf.Lerp(ShowInfo.Opacity, HideInfo.Opacity, timer / windowMoveDuration);



//         }
//         // IEnumerator ChangeGUI(GUI_Window_Info window_Info) { 



//         //     yield break; 
//         // }

//     }




// #if UNITY_EDITOR
//     [CustomEditor(typeof(ShowHideGuiController))]
//     public class CustomUIControllerEditor : Editor
//     {


//         public override void OnInspectorGUI()
//         {
//             base.OnInspectorGUI();

//             ShowHideGuiController customUIController = (ShowHideGuiController)target;

//             if (GUILayout.Button("ShowGui")) {
//                 customUIController.ShowGui();
//             }

//             if (GUILayout.Button("HideGui")) {
//                 customUIController.HideGui();
//             }

//             if (GUILayout.Button("ShowGuiLerp")) {
//                 customUIController.ShowGuiLerp();
//             }

//             if (GUILayout.Button("HideGuiLerp")) {
//                 customUIController.HideGuiLerp();
//             }


//         }

//         public void OnSceneGUI()
//         {
//             ShowHideGuiController customUIController = (ShowHideGuiController)target;

//             RectTransform recttrans = customUIController.transform.GetComponent<RectTransform>();
//             RectTransform parentrecttrans = customUIController.transform.parent.GetComponent<RectTransform>();

//             // Debug.Log(parentrecttrans.sizeDelta.x);

//             // Debug.Log(recttrans.anchoredPosition);

//             // Debug.Log(recttrans.anchoredPosition);
//             // Debug.Log("Anchor Min" +recttrans.anchorMin);
//             // Debug.Log("Anchor Max" +recttrans.anchorMax);

//             Vector3 transformPosition = customUIController.transform.parent.position;

//             float scale = customUIController.transform.parent.localScale.x;


//             float xxx = (recttrans.anchorMin.x + recttrans.anchorMax.x) / 2;
//             float yyy = (recttrans.anchorMin.y + recttrans.anchorMax.y) / 2;
//             transformPosition.x += Mathf.Lerp(-parentrecttrans.sizeDelta.x / 2, parentrecttrans.sizeDelta.x / 2, xxx) * scale;
//             transformPosition.y += Mathf.Lerp(-parentrecttrans.sizeDelta.y / 2, parentrecttrans.sizeDelta.y / 2, yyy) * scale;



//             Handles.color = Color.green;
//             Handles.DrawWireCube(transformPosition + (customUIController.ShowInfo.Position * scale), Vector3.one * .5f);

//             EditorGUI.BeginChangeCheck();
//             Vector3 newPosition = Handles.PositionHandle(transformPosition + (customUIController.ShowInfo.Position * scale), Quaternion.identity);
//             if (EditorGUI.EndChangeCheck()) {
//                 Undo.RecordObject(customUIController, "Change Anchor Position");
//                 customUIController.ShowInfo.Position = (newPosition - transformPosition) / scale;
//                 serializedObject.Update();
//             }


//             Handles.color = Color.red;
//             Handles.DrawWireCube(transformPosition + (customUIController.HideInfo.Position * scale), Vector3.one * .5f);

//             EditorGUI.BeginChangeCheck();
//             newPosition = Handles.PositionHandle(transformPosition + customUIController.HideInfo.Position * scale, Quaternion.identity);
//             if (EditorGUI.EndChangeCheck()) {
//                 Undo.RecordObject(customUIController, "Change Anchor Position");
//                 customUIController.HideInfo.Position = (newPosition - transformPosition) / scale;
//                 serializedObject.Update();
//             }

//             Handles.DrawLine(transformPosition + customUIController.ShowInfo.Position * scale,
//             transformPosition + customUIController.HideInfo.Position * scale
//             );

//         }



//     }

// #endif

// }