

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableToRemove : MonoBehaviour, IRemovable
{

    // [SerializeField] GameObject GameObjectToDisable; 
    public void Remove()
    {
        gameObject.SetActive(false); 
    }

    // public void RemoveNextFrame()
    // {
    //     StartCoroutine(DisableCoroutine());
    // }

    // IEnumerator DisableCoroutine() {
    //     yield return null; 
    //     gameObject.SetActive(false); 
    // }
}