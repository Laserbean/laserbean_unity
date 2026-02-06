using Laserbean.General;
using UnityEngine;

using Laserbean.General.Follower;
using UnityEngine.Events;

namespace Laserbean.Input.WorldDrag
{
    public class WorldDragForce2D : SmoothFollowRigidbody2D, IWorldDraggable
    {
        [SerializeField] UnityEvent<Vector3, Transform> OnDragEvent;
        [SerializeField] UnityEvent<Transform> OnDragReleasedEvent;
        public void Drag(Vector3 mouseLocation)
        {
            SetTarget(mouseLocation);
            StartFollowing();
            OnDragEvent?.Invoke(mouseLocation, transform);

        }

        public void DragReleased()
        {
            StopFollowing();
            OnDragReleasedEvent?.Invoke(transform);

        }
    }
}