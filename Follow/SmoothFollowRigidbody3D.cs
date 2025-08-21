using Laserbean.General;
using UnityEngine;


namespace Laserbean.General.Follower
{
    [RequireComponent(typeof(Rigidbody))]
    public class SmoothFollowRigidbody3D : SmoothFollow
    {
        Rigidbody rgbd;
        void Awake()
        {
            rgbd = GetComponent<Rigidbody>();
        }

        public override void AddForce(Vector3 force)
        {
            base.AddForce(force);
            rgbd.AddForce(force);
        }


    }
}
