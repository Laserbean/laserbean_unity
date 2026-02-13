using UnityEngine;
namespace Laserbean.Input.WorldDrag
{
    public interface IWorldDraggable
    {
        public void Drag(Vector3 mouseLocation);
        public void DragReleased();
        public bool DragStarted();

    }
}