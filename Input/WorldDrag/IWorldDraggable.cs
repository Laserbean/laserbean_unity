using UnityEngine;

public interface IWorldDraggable
{
    public void Drag(Vector3 mouseLocation);
    public void DragReleased();

}