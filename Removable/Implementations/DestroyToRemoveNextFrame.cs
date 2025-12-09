
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.Removable
{
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
}