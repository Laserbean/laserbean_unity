using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General;
using UnityEngine.PlayerLoop;

public class PlayerInteractions : MonoBehaviour
{
    public Transform focusTransform; 

    private void Awake() {
        if (focusTransform == null) {
            focusTransform = this.transform; 
            Debug.LogWarning("Focus is the player itself"); 
        }
    }


    GameObject closestInteractable = null; 

    public void OnInteract() {

        // GameObject closest = GetClosestInteractable(); 

        closestInteractable?.GetComponent<IInteractable>()?.Interact(this.gameObject); 
        closestInteractable = null; 
    }


    private void Update() {
        float distance = 1000f;
        
        foreach(GameObject thing in interactableList) {
            float curdistance = (thing.transform.position - focusTransform.position).sqrMagnitude; 
            if (curdistance < distance) {
                distance = curdistance;
                closestInteractable?.GetComponent<IInteractable>()?.Highlight(false); 
                closestInteractable = thing; 
                closestInteractable?.GetComponent<IInteractable>()?.Highlight(true); 
            }
            

        }        
    }

    public List<GameObject> interactableList = new (); 

    private void OnTriggerEnter2D(Collider2D other) {
        // if (other.gameObject.HasTag(Constants.TAG_INTERACTABLE)) {
        var interactt = other.gameObject.GetComponent<IInteractable>(); 
        if (interactt != null) {
            interactableList.Add(other.gameObject); 
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (interactableList.Contains(other.gameObject)) {
            interactableList.Remove(other.gameObject); 
            other.gameObject.GetComponent<IInteractable>().Highlight(false);
        }
    }
}
