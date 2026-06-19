using System.Collections.Generic;
using System.Linq;
using Laserbean.Input.WorldDrag;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class WorldInteractableController : MonoBehaviour, IWorldInteractable
{
    public List<IWorldInteractable> worldDraggables = new();


    public void SetInteractable(bool canInteract)
    {
        foreach (var child in worldDraggables)
        {
            child.SetInteractable(canInteract);
        }

        if (canInteract)
        {
            OnIsDraggable.Invoke();
        }
        else
        {
            OnNotDraggable.Invoke();
        }
    }

    void Start()
    {
        worldDraggables = GetComponentsInChildren<IWorldInteractable>().ToList();
        worldDraggables.Remove(this);
    }


    [SerializeField] UnityEvent OnNotDraggable;
    [SerializeField] UnityEvent OnIsDraggable;


}
