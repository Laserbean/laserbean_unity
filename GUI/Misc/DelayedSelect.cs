using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using UnityEngine;


using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Laserbean.CustomGUI
{
    public class DelayedSelect : MonoBehaviour
    {
        Selectable selectable;
        void Start()
        {
            selectable = GetComponent<Selectable>();
        }

        // public void Select()
        // {
        //     EventSystem.current.SetSelectedGameObject(selectable.gameObject);
        // }

        public void SelectNextFrame()
        {
            StartCoroutine(SelectRoutine());
        }

        public void SelectNextFrameNoEventTrigger()
        {
            StartCoroutine(SelectRoutine2());
        }

        IEnumerator SelectRoutine()
        {
            yield return null;
            selectable.Select();
        }
        
        IEnumerator SelectRoutine2()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(selectable.gameObject);
        }
    }
}
