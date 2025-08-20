using UnityEngine;

public class SimpleWorldDrag : MonoBehaviour, IWorldDraggable
{
    public void Drag(Vector2 mouseLocation)
    {
        transform.position = mouseLocation; 
    }

    
}
