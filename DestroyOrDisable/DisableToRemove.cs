

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableToRemove : MonoBehaviour, IDestroyOrDisable
{

    // [SerializeField] GameObject GameObjectToDisable; 
    void IDestroyOrDisable.DestroyOrDisable()
    {
        gameObject.SetActive(false); 
    }

    void IDestroyOrDisable.DestroyOrDisableNextFrame()
    {
        StartCoroutine(DisableCoroutine());
    }

    IEnumerator DisableCoroutine() {
        yield return null; 
        gameObject.SetActive(false); 
    }
}