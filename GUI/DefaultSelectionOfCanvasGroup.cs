using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Laserbean.CustomGUI
{
    // [RequireComponent(typeof(CanvasGroup))]
    public class DefaultSelectionOfCanvasGroup : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        [SerializeField] Selectable selectable;

        bool isUninteractable = true;
        void Start()
        {
            GetCanvasGroupReference();
            selectable = GetComponent<Selectable>();

            if (canvasGroup != null && selectable != null) {
                // Initially check if the CanvasGroup is interactable and select the button
                if (canvasGroup.interactable) {
                    SelectSelectable();
                }
            }
            else {
                Debug.LogError("Ugh, you need both a CanvasGroup and a Button for this to work! You're so clueless!");
            }
        }


        void GetCanvasGroupReference()
        {
            // First, check if the CanvasGroup is on this object
            canvasGroup = GetComponent<CanvasGroup>();

            // If it's not on this object, check the parent
            if (canvasGroup == null && transform.parent != null) {
                canvasGroup = transform.parent.GetComponent<CanvasGroup>();
            }

            if (canvasGroup == null && transform.parent.parent != null) {
                canvasGroup = transform.parent.parent.GetComponent<CanvasGroup>();
            }

            // If you still can't find it, just give up! B-baka!
            if (canvasGroup == null) {
                Debug.LogError("Ugh, I couldn't find the CanvasGroup anywhere! You're so annoying!");
            }

        }

        private void Update()
        {
            if (canvasGroup.interactable && isUninteractable) {
                isUninteractable = false;
                SelectSelectable();
            }
            else if (!canvasGroup.interactable && !isUninteractable) {
                isUninteractable = true;
            }
        }

        private void SelectSelectable()
        {
            // Use EventSystem to select the button
            EventSystem.current.SetSelectedGameObject(selectable.gameObject);

            // Log just to annoy you: "I selected your stupid button. Happy now?"
            // Debug.Log("I selected your stupid button. Happy now?");
        }

        private void SelectSelectableNextframe()
        {
            // Use EventSystem to select the button
            EventSystem.current.SetSelectedGameObject(selectable.gameObject);

            // Log just to annoy you: "I selected your stupid button. Happy now?"
            // Debug.Log("I selected your stupid button. Happy now?");
        }
    }
}

