using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General;


public class PlayerInteractions : MonoBehaviour
{
    public Transform focusTransform; 

    private void Awake() {
        if (focusTransform == null) {
            focusTransform = this.transform; 
            Debug.LogWarning("Focus is the player itself"); 
        }
    }


    public void OnInteract() {
        float distance = 1000f;
        float curdistance = 1000f;
        GameObject closest = null; 
        foreach(GameObject thing in interactableList) {
            curdistance = (thing.transform.position - focusTransform.position).sqrMagnitude; 
            if (curdistance < distance) {
                distance = curdistance;
                closest = thing; 
            }
        }
        closest?.GetComponent<IInteractable>()?.Interact(this.gameObject); 
    }

    public List<GameObject> interactableList = new List<GameObject>(); 

    private void OnTriggerEnter2D(Collider2D other) {
        // if (other.gameObject.HasTag(Constants.TAG_INTERACTABLE)) {
        var interactt = other.gameObject.GetComponent<IInteractable>(); 
        if (interactt != null) {
            interactableList.Add(other.gameObject); 
            interactt.Highlight(true); 
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (interactableList.Contains(other.gameObject)) {
            interactableList.Remove(other.gameObject); 
            other.gameObject.GetComponent<IInteractable>().Highlight(false);
        }
    }
}
