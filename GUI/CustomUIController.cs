using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomUIController : MonoBehaviour
{

   public Vector3 TEST = Vector3.zero; 
}

[System.Serializable]
public class GUI_State_Control {
    public GameObject guiObject;

    public GUI_Window_Info ShowInfo;
    public GUI_Window_Info HideInfo; 

}

