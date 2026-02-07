using UnityEngine;

namespace Laserbean.General.Follower
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SmoothFollowRigidbody2D : SmoothPosFollower
    {
        Rigidbody2D rgbd2d;

        protected override void Awake()
        {
            base.Awake(); 
            rgbd2d = GetComponent<Rigidbody2D>();
        }
        public override void AddForce(Vector3 force)
        {
            base.AddForce(force);
            rgbd2d.AddForce(force);
        }

    }
}

