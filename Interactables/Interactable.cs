using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Laserbean.General;
using UnityEngine.PlayerLoop;

public class Interactable : MonoBehaviour, IInteractable
{

    [SerializeField] UnityEvent OnInteract;
    [SerializeField] UnityEvent OnHighlight; 
    [SerializeField] UnityEvent OnUnHighlight; 

    private void Start() {
        var collider2d = this.GetComponent<Collider2D>(); 
        if (collider2d == null) {
            this.gameObject.AddComponent<CircleCollider2D>().isTrigger = true; 
        }
    }

    void IInteractable.Highlight()
    {
        OnHighlight?.Invoke(); 
    }
        void IInteractable.UnHighlight()
    {
        OnUnHighlight?.Invoke(); 
    }

    void IInteractable.Interact(GameObject gameObject)
    {
        OnInteract?.Invoke(); 
    }


}