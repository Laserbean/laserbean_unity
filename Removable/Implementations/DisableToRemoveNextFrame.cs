

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.Removable
{
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
}