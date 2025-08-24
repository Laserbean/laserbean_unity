using Laserbean.General;
using UnityEngine;

using Laserbean.General.Follower;
public class WorldDragForce2D : SmoothFollowRigidbody2D, IWorldDraggable
{
    public void Drag(Vector3 mouseLocation)
    {
        StartFollowing();
        SetTarget(mouseLocation);

    }

    public void DragReleased()
    {
        StopFollowing();

    }
}
