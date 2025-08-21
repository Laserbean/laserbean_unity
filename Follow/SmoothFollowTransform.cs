using Laserbean.General;
using Unity.Cinemachine;
using UnityEngine;

namespace Laserbean.General.Follower
{
    public class SmoothFollowTransform : SmoothFollow
    {
        Vector3 cur_speed = Vector3.zero;

        public override void AddForce(Vector3 force)
        {
            base.AddForce(force);
            cur_speed += force;
            transform.position = transform.position + cur_speed * Time.fixedDeltaTime;
        }
    }
}
