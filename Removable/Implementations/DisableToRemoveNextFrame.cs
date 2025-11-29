

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableToRemoveNextFrame : MonoBehaviour, IRemovable
{

    // [SerializeField] GameObject GameObjectToDisable; 
    public void Remove()
    {
        StartCoroutine(DisableCoroutine());
    }

    IEnumerator DisableCoroutine()
    {
        yield return null;
        gameObject.SetActive(false);
    }
}