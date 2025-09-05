using UnityEngine;

namespace Laserbean.General.Follower
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SmoothFollowRigidbody2D : SmoothFollow
    {
        Rigidbody2D rgbd2d;

        void Awake()
        {
            rgbd2d = GetComponent<Rigidbody2D>();
        }
        public override void AddForce(Vector3 force)
        {
            base.AddForce(force);
            rgbd2d.AddForce(force);
        }

    }
}

