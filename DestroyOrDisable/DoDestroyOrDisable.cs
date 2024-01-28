using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDestroyOrDisable : MonoBehaviour
{
    public void DestroyOrDisableInParent()
    {
        GetComponentInParent<IDestroyOrDisable>()?.DestroyOrDisable();
    }

    public void DestroyOrDisableInChildren()
    {
        GetComponentInChildren<IDestroyOrDisable>()?.DestroyOrDisable();
    }
}
