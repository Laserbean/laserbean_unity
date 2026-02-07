using System.Collections.Generic;
using UnityEngine;


namespace Laserbean.General.Follower
{
    public abstract class SmoothFollow : MonoBehaviour, IPosFollower //, ILocalPositionFollower
    {
        [Header("PID Constants")]
        [SerializeField] protected float p_const = 0.10f;
        [SerializeField] protected float i_const = 0.1f;
        [SerializeField] protected float d_const = 0.1f;
        [SerializeField] FollowTarget Target = new(Vector3.zero);


        [Header("Integral Buffer Size"), Range(0, 10000)]
        [SerializeField] float buffer_size = 64;

        [Header("Lock Axis")]
        [SerializeField] private bool Lock_x = false;
        [SerializeField] private bool Lock_y = false;
        [SerializeField] private bool Lock_z = false;

        [Header("Stop When Close")]
        [SerializeField] private bool Stop_when_close = true;

        [SerializeField] float distance_treshold = 0.1f;

        private Queue<Vector3> total_error_buffer = new();

        // [SerializeField] bool isFollowLocalPos = false;
        // Vector3 fixedLocalPosition = Vector3.zero;

        private Vector3 Derivative
        {
            get
            {
                return (current_error - previous_error) / Time.fixedDeltaTime;
            }
        }

        Vector3 previous_error = Vector3.zero;
        Vector3 current_error = Vector3.zero;
        Vector3 total_error = Vector3.zero;


        protected virtual void Awake()
        {
            // IsFollowing = false;

            // fixedLocalPosition = transform.localPosition;
        }

        Vector3 TargetPosition
        {
            get
            {
                // if (isFollowLocalPos)
                // {
                //     return transform.parent != null ? transform.parent.TransformPoint(fixedLocalPosition) : transform.TransformPoint(fixedLocalPosition);
                // }

                return Target.Position;
            }
        }



        // public void SetLocalPositionTarget(Vector3 targetpos)
        // {
        //     fixedLocalPosition = targetpos;
        // }

        private bool IsAtTarget
        {
            get
            {
                return (TargetPosition - transform.position).sqrMagnitude < distance_treshold * distance_treshold;
            }
        }

        [SerializeField] bool IsFollowing = true;

        public void StartFollowing()
        {
            IsFollowing = true;
        }

        public void StopFollowing()
        {
            current_error = Vector3.zero;

            IsFollowing = false;
        }


        public void FixedUpdate()
        {
            if (!IsFollowing)
            {
                return;
            }

            previous_error = current_error;
            // PID control
            current_error = TargetPosition - transform.position;

            Vector3 integral = current_error * Time.fixedDeltaTime; // Simple integral approximation

            if (buffer_size > 0)
            {
                if (total_error_buffer.Count >= buffer_size)
                {
                    total_error -= total_error_buffer.Dequeue();
                }
                total_error_buffer.Enqueue(integral);
            }
            total_error += integral;

            // Debug.Log((current_error + " " + total_error + " " + Derivative).DebugColor(Color.green));

            Vector3 pidForce = (p_const * current_error) + (i_const * total_error) + (d_const * Derivative);

            if (IsAtTarget && Stop_when_close) return;


            if (Lock_x) pidForce.x = 0f;
            if (Lock_y) pidForce.y = 0f;
            if (Lock_z) pidForce.z = 0f;
            AddForce(pidForce);
        }

        public virtual void AddForce(Vector3 force)
        {
            if (force == Vector3.zero) return;
        }


        public void AddTarget(FollowTarget target)
        {
            Target = target;
        }

        public void RemoveTarget(FollowTarget target)
        {
            Target = null;
        }

        public void ClearTargets()
        {
            Target = null;
        }

        public bool HasTarget(FollowTarget target)
        {
            return target == Target;
        }
    }
}