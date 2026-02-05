using Laserbean.General;
using UnityEngine;
using UnityEngine.Events;

public class SimpleWorldHover : MonoBehaviour, IHoverable
{
    [SerializeField] UnityEvent OnHoverEnterEvent;
    [SerializeField] UnityEvent OnHoverExitEvent;
    [SerializeField] UnityEvent<Vector2> OnHoverEvent;
    public void Hover(Vector2 mouseLocation)
    {
        OnHoverEvent.Invoke(mouseLocation);
    }

    public void OnHoverEnter()
    {
        // Debug.Log("Hover Enter!".DebugColor(Color.green));
        OnHoverEnterEvent?.Invoke();
    }

    public void OnHoverExit()
    {
        // Debug.Log("Hover Exit!".DebugColor(Color.red));
        OnHoverExitEvent?.Invoke();
    }


}
