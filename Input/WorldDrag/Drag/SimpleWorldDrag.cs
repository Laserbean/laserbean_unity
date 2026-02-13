using UnityEngine;

namespace Laserbean.Input.WorldDrag
{
    public class SimpleWorldDrag : MonoBehaviour, IWorldDraggable
    {
        public void Drag(Vector3 mouseLocation)
        {
            transform.position = mouseLocation;
        }

        public void DragReleased()
        {
        }


        public bool DragStarted()
        {
            return true;
        }


    }
}