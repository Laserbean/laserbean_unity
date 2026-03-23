using Laserbean.General;
using UnityEngine;
using UnityEngine.Events;

public class SimpleWorldHover : MonoBehaviour, IHoverable
{
    [SerializeField] UnityEvent OnHoverEnterEvent;
    [SerializeField] UnityEvent OnHoverExitEvent;
    [SerializeField] UnityEvent<Vector2> OnHoverEvent;

    [SerializeField] CursorType cursorType;

    public void Hover(Vector2 mouseLocation)
    {
        OnHoverEvent.Invoke(mouseLocation);
    }

    public void OnHoverEnter()
    {
        // Debug.Log("Hover Enter!".DebugColor(Color.green));
        OnHoverEnterEvent?.Invoke();
        // CustomCursor.Instance.SetCursorByType(cursorType);

    }

    public void OnHoverExit()
    {
        // Debug.Log("Hover Exit!".DebugColor(Color.red));
        OnHoverExitEvent?.Invoke();
        // CustomCursor.Instance.ResetCursor();

    }

}


[System.Serializable]
public enum CursorType
{
    None = 0,
    Default = 1,
    Uninteractable = 2,

    Clicking = 3,
    Clickable = 4,
    Dragging = 5,

    Draggable = 6,
    Panning = 7, 
    
}