
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyToRemove : MonoBehaviour, IDestroyOrDisable
{
    void IDestroyOrDisable.DestroyOrDisable()
    {
        Destroy(gameObject); 
    }
}