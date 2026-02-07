using Laserbean.General;
using UnityEngine;

using Laserbean.General.Follower;
using UnityEngine.InputSystem;

namespace Laserbean.Input.WorldDrag
{
    public class WorldDragForce3D : SmoothFollowRigidbody3D, IWorldDraggable
    {

        PosFollowTarget mousetarget = new(Vector3.zero);

        protected override void Awake()
        {
            // AddTarget(mousetarget);
        }

        public void Drag(Vector3 mouseLocation)
        {
            mousetarget.Position = mouseLocation;
            Targets.TryAddTarget(mousetarget);
            // targett.z = transform.position.z; 
        }

        public void DragReleased()
        {
            Targets.RemoveTarget(mousetarget);
        }
    }
}
