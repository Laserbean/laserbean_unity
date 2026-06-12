using UnityEngine;
using UnityEngine.Events;

namespace Laserbean.Input.WorldDrag
{
    public class SimpleWorldDrag : MonoBehaviour, IWorldDraggable
    {
        public void Drag(Vector3 mouseLocation)
        {
            transform.position = mouseLocation;
            OnDrag.Invoke(mouseLocation);

        }

        public void DragReleased()
        {
            OnDragStop.Invoke(); 
        }


        public bool DragStarted()
        {
            OnDragStart.Invoke();
            return true;
        }

        [SerializeField] UnityEvent OnDragStart;
        [SerializeField] UnityEvent<Vector3> OnDrag;
        [SerializeField] UnityEvent OnDragStop;

    }
}