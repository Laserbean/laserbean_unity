

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableToRemove : MonoBehaviour, IDestroyOrDisable
{

    // [SerializeField] GameObject GameObjectToDisable; 
    public void DestroyOrDisable()
    {
        gameObject.SetActive(false); 
    }

    public void DestroyOrDisableNextFrame()
    {
        StartCoroutine(DisableCoroutine());
    }

    IEnumerator DisableCoroutine() {
        yield return null; 
        gameObject.SetActive(false); 
    }
}