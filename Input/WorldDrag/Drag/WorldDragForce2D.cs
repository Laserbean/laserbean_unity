using Laserbean.General;
using UnityEngine;

using Laserbean.General.Follower;
using UnityEngine.Events;

namespace Laserbean.Input.WorldDrag
{
    public class WorldDragForce2D : SmoothFollowRigidbody2D, IWorldDraggable
    {

        PosFollowTarget mousetarget = new(Vector3.zero);

        [SerializeField] UnityEvent<Vector3, Transform> OnDragEvent;
        [SerializeField] UnityEvent<Transform> OnDragReleasedEvent;

        public void Drag(Vector3 mouseLocation)
        {
            mousetarget.Position = mouseLocation;
            Targets.TryAddTarget(mousetarget);
            OnDragEvent?.Invoke(mouseLocation, transform);
        }

        public void DragReleased()
        {
            OnDragReleasedEvent?.Invoke(transform);
            Targets.RemoveTarget(mousetarget);

        }
    }
}