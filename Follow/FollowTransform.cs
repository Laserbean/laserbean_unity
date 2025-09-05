
using UnityEngine;

namespace Laserbean.General.Follower
{
    public class FollowTransform : SmoothFollow
    {

        public override void AddForce(Vector3 force)
        {
            base.AddForce(force);
            transform.position += force;
        }
    }
}
