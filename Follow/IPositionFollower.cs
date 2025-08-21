
using UnityEngine;


namespace Laserbean.General.Follower
{
    public interface IPositionFollower
    {
        public void SetTarget(Vector3 targetpos);
        public void SetTarget(Transform target);

        public void RemoveTargetTransform();

        public void RemoveTargetPosition();
    }

    // public interface ILocalPositionFollower
    // {
    //     public void SetLocalPositionTarget(Vector3 targetpos);
    // }
}