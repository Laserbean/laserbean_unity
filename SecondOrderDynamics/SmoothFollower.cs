using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General
{

    public class SmoothFollower : MonoBehaviour
    {
        [SerializeField] Transform followTransform;


        [SerializeField] float frequency = 1f;
        [SerializeField] float damping = 1f;
        [SerializeField] float response = 1f;

        SecondOrderDynamics secondOrderDynamics;


        Vector3 Relative_position => followTransform.position - transform.position;


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
            rigidbody2D.velocity = secondOrderDynamics.Step(Time.fixedDeltaTime, Relative_position, rigidbody2D.velocity);
        }

    }
}