using UnityEngine;


namespace Laserbean.General.Follower
{
    [RequireComponent(typeof(Rigidbody))]
    public class SmoothFollowRigidbody3D : SmoothPosFollower
    {
        Rigidbody rgbd;
        protected override void Awake()
        {
            base.Awake(); 

            rgbd = GetComponent<Rigidbody>();
        }

        public override void AddForce(Vector3 force)
        {
            base.AddForce(force);
            rgbd.AddForce(force);
        }


    }
}
