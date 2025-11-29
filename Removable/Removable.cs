using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removable : MonoBehaviour
{
    public void RemoveFromParent()
    {
        GetComponentInParent<IRemovable>()?.Remove();
    }

    public void RemoveFromChildren()
    {
        GetComponentInChildren<IRemovable>()?.Remove();
    }
}
