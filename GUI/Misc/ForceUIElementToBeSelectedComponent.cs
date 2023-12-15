using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using UnityEngine;


using UnityEngine.EventSystems;
using UnityEngine.UI;

//Note: This was stolen from unity forums.

namespace Laserbean.CustomGUI
{
    public class ForceUIElementToBeSelectedComponent : MonoBehaviour
    {
        private EventSystem currentEventSystem;
        private GameObject currentlySelected;

        CanvasGroup canvasGroup; 

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            // GetCanvasGroupReference();
        }

        void GetCanvasGroupReference()
        {
            // First, check if the CanvasGroup is on this object
            canvasGroup = GetComponent<CanvasGroup>();

            // If it's not on this object, check the parent
            if (canvasGroup == null && transform.parent != null) {
                canvasGroup = transform.parent.GetComponent<CanvasGroup>();
            }

            // If you still can't find it, just give up! B-baka!
            if (canvasGroup == null) {
                Debug.LogError("Ugh, I couldn't find the CanvasGroup anywhere! You're so annoying!");
            }
 
        }

        private void Start()
        {
            currentEventSystem = EventSystem.current;
            currentlySelected = currentEventSystem.currentSelectedGameObject;
        }

        private void Update()
        {
            //Check if the last known selected GameObject has changed since
            //the last frame

            // if (!canvasGroup.interactable) return; 

            if (currentEventSystem.currentSelectedGameObject != null &&
                currentlySelected != currentEventSystem.currentSelectedGameObject) {
                currentlySelected = currentEventSystem.currentSelectedGameObject;
            }

            // The currentSelectedGameObject will be null when you click with your
            // anywhere on the screen on a non-Selectable GameObject.
            if (currentEventSystem.currentSelectedGameObject == null) {
                // If this happens simply re-select the last known selected GameObject.
                if (currentlySelected != null) {
                    currentlySelected.GetComponent<Selectable>().Select();
                }
                else {
                    // If there is none, select the firstSelectedGameObject
                    // (which can be setup inthe EventSystem component).
                    currentlySelected = currentEventSystem.firstSelectedGameObject;
                    currentlySelected.GetComponent<Selectable>().Select();
                }
            }
        }
    }
}