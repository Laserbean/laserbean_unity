using UnityEngine;

public class SimpleWorldDrag : MonoBehaviour, IWorldDraggable
{
    public void Drag(Vector3 mouseLocation)
    {
        transform.position = mouseLocation; 
    }

    public void DragReleased()
    {
    }
}
