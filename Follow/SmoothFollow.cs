using System.Collections.Generic;
using UnityEngine;


namespace Laserbean.General.Follower
{
    public abstract class SmoothFollow : MonoBehaviour, IPositionFollower //, ILocalPositionFollower
    {
        [Header("PID Constants")]
        [SerializeField] protected float p_const = 0.10f;
        [SerializeField] protected float i_const = 0.1f;
        [SerializeField] protected float d_const = 0.1f;
        [SerializeField] public Transform Target;

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
        Vector3 target_position;


        void Awake()
        {
            target_position = transform.position;

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
                if (Target == null)
                {
                    return target_position;
                }
                return Target.transform.position;
            }
        }

        public void SetTarget(Vector3 targetpos)
        {
            target_position = targetpos;
        }

        public void SetTarget(Transform target)
        {
            Target = target;
        }

        public void RemoveTargetTransform()
        {
            Target = null;
        }

        public void RemoveTargetPosition()
        {
            target_position = transform.position;
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

        public void FixedUpdate()
        {
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


    }
}