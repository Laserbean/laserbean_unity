using UnityEngine;

namespace Laserbean.General.Follower
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SmoothFollowRigidbody2D : SmoothPosFollower
    {
        Rigidbody2D rgbd2d;

        [SerializeField] bool counteractGravity = true;

        protected override void Awake()
        {
            base.Awake();
            rgbd2d = GetComponent<Rigidbody2D>();
        }
        public override void AddForce(Vector3 force)
        {
            var fff = counteractGravity ? force - (Physics2D.gravity * rgbd2d.mass * rgbd2d.gravityScale).ToVector3() : force;
            base.AddForce(fff);
            rgbd2d.AddForce(fff);
        }

    }
}

