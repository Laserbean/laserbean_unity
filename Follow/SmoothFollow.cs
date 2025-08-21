using System.Collections.Generic;
using Laserbean.General;
using Unity.Cinemachine;
using UnityEngine;


namespace Laserbean.General.Follower
{
    public abstract class SmoothFollow : MonoBehaviour
    {
        [SerializeField] float p_const = 0.10f;
        [SerializeField] float i_const = 0.1f;
        [SerializeField] float d_const = 0.1f;
        [SerializeField] public Transform Target;

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

        [SerializeField] private bool Lock_x = false;
        [SerializeField] private bool Lock_y = false;
        [SerializeField] private bool Lock_z = false;

        [SerializeField] private bool Stop_when_close = true;


        [SerializeField] float distance_treshold = 0.1f;


        private bool IsAtTarget
        {
            get
            {
                return (Target.transform.position - transform.position).sqrMagnitude < distance_treshold * distance_treshold;
            }
        }

        private Queue<Vector3> total_error_buffer = new();
        [SerializeField] float buffer_size = 64;

        public void FixedUpdate()
        {

            previous_error = current_error;
            // PID control
            current_error = Target.position - transform.position;

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