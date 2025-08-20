using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General
{

    public class SmoothRotationFollower : MonoBehaviour
    {
        [SerializeField] Transform followTransform;


        [SerializeField] float frequency = 1f;
        [SerializeField] float damping = 1f;
        [SerializeField] float response = 1f;

        SecondOrderDynamicsFloat secondOrderDynamics;


        float Relative_position => followTransform.rotation.eulerAngles.z - transform.rotation.eulerAngles.z;


        new Rigidbody2D rigidbody2D;
        void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            secondOrderDynamics = new(frequency, damping, response, Relative_position);
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            secondOrderDynamics = new(frequency, damping, response, Relative_position);
        }
#endif

        void FixedUpdate()
        {
            // rigidbody2D.linearVelocity = secondOrderDynamics.Step(Time.fixedDeltaTime, Relative_position, rigidbody2D.linearVelocity);
            rigidbody2D.angularVelocity = secondOrderDynamics.Step(Time.fixedDeltaTime, Relative_position, rigidbody2D.angularVelocity);
        }

    }
}