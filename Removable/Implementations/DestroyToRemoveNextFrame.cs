
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyToRemoveNextFrame : MonoBehaviour, IRemovable
{
    public void Remove()
    {
        StartCoroutine(DestroyCoroutine());

    }
    IEnumerator DestroyCoroutine()
    {
        yield return null;
        Destroy(gameObject);
    }
}