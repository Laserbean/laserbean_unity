using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugInspectorButton : MonoBehaviour
{

    public List<UnityEventThing> Events = new();

}

[System.Serializable]
public class UnityEventThing
{

    public UnityEvent unityEvent;
    public string Name;

}


