using Laserbean.General;
using UnityEngine;

using Laserbean.General.Follower;
using UnityEngine.Events;

namespace Laserbean.Input.WorldDrag
{
    public class WorldDragForce2D : SmoothFollowRigidbody2D, IWorldDraggable
    {

        FollowTarget mousetarget = new(Vector3.zero);

        // [SerializeField] UnityEvent<Vector3, Transform> OnDragEvent;
        // [SerializeField] UnityEvent<Transform> OnDragTransformEvent;
        // [SerializeField] UnityEvent<Transform> OnDragStartTransformEvent;

        // [SerializeField] UnityEvent<Transform> OnDragReleasedEvent;

        public void Drag(Vector3 mouseLocation)
        {
   
            mousetarget.Position = mouseLocation;
            Targets.TryAddTarget(mousetarget);
            // OnDragEvent?.Invoke(mouseLocation, transform);
            // OnDragTransformEvent?.Invoke(transform);
        }

        public void DragReleased()
        {
            // OnDragReleasedEvent?.Invoke(transform);
            Targets.RemoveTarget(mousetarget);
        }

        public bool DragStarted()
        {
            return true; 
            // OnDragStartTransformEvent?.Invoke(transform);
        }
    }
}