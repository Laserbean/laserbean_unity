using System.Collections;
using UnityEngine;

namespace Laserbean.Removable
{
    public class RemoveAfter : MonoBehaviour
    {
        [SerializeField] bool StartOnEnable = false;
        [SerializeField] float DisableAfterTime = 3f;

        private void OnEnable()
        {
            if (StartOnEnable)
            {
                StartCoroutine(RemoveAfterSeconds(DisableAfterTime));
            }
        }

        public void StartRemoveAfter()
        {
            StartCoroutine(RemoveAfterSeconds(DisableAfterTime));
        }

        public void StartRemoveAfter(float seconds)
        {
            StartCoroutine(RemoveAfterSeconds(seconds));
        }

        IEnumerator RemoveAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            GetComponent<IRemovable>().Remove();
            // gameObject.SetActive(false);
        }
    }
}