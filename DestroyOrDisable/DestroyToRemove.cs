
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyToRemove : MonoBehaviour, IDestroyOrDisable
{
    public void DestroyOrDisable()
    {
        Destroy(gameObject); 
    }

    public void DestroyOrDisableNextFrame()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine() {
        yield return null; 
        Destroy(gameObject); 
    }
}