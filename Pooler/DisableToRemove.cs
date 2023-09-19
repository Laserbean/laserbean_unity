

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableToRemove : MonoBehaviour, IDestroyOrDisable
{
    void IDestroyOrDisable.DestroyOrDisable()
    {
        gameObject.SetActive(false); 
    }
}