
using UnityEngine;

namespace Laserbean.General.Follower
{
    public class FollowTransform : SmoothFollow
    {

        void Awake()
        {
            d_const = 0;
        }

        public override void AddForce(Vector3 force)
        {
            base.AddForce(force);
            transform.position += force;
        }
    }
}
