
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyToRemove : MonoBehaviour, IRemovable
{
    public void Remove()
    {
        Destroy(gameObject); 
    }

    // public void RemoveNextFrame()
    // {
    //     StartCoroutine(DestroyCoroutine());
    // }

    // IEnumerator DestroyCoroutine() {
    //     yield return null; 
    //     Destroy(gameObject); 
    // }
}