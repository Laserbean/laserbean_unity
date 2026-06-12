using UnityEngine;
namespace Laserbean.Input.WorldDrag
{
    public interface IWorldDraggable
    {
        public void Drag(Vector3 mouseLocation);
        public void DragReleased();
        public bool DragStarted();

    }

    public interface IWorldDragOver
    {
        public void DragOver(Transform transform); 
        public void DragExit(Transform transform); 

        public void DragReleased(Transform transform); 
        // public void DragStarted(Transform transform); 
    }
}