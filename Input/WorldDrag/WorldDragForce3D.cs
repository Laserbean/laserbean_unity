using Laserbean.General;
using UnityEngine;

using Laserbean.General.Follower;
public class WorldDragForce3D : SmoothFollowRigidbody3D, IWorldDraggable
{
    public void Drag(Vector3 mouseLocation)
    {
        var targett = mouseLocation;
        // targett.z = transform.position.z; 
        StartFollowing();
        SetTarget(targett);
    }

    public void DragReleased()
    {
        StopFollowing();
    }
}
