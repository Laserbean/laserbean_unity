using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.PlayerUtils
{

    public class PlayerPositionSetter : MonoBehaviour
    {
        new Rigidbody2D rigidbody2D;

        [SerializeField] float FuturePositionSensitivity = 2f;
        private void Awake()
        {
            PlayerPosition.PlayerTransform = this.transform;
            rigidbody2D = GetComponent<Rigidbody2D>();

            PlayerPosition.Sensitivity = FuturePositionSensitivity;
        }

        private void Update()
        {
            PlayerPosition.AddCurrentVelocity(rigidbody2D.velocity);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            PlayerPosition.Sensitivity = FuturePositionSensitivity;
        }

#endif


        private void OnDrawGizmos()
        {
            if (PlayerPosition.PlayerTransform != null)
                Gizmos.DrawCube(PlayerPosition.FuturePosition, Vector3.one);
        }

    }



    public static class PlayerPosition
    {
        public static Transform PlayerTransform;


        public static Vector3 Position {
            get => PlayerTransform.position;
        }


        public static Vector3 FuturePosition {
            get {
                return Position + (AverageVelocity * Sensitivity);
            }
        }

        static int index = 0;

        public static float Sensitivity = 2f;

        const int buffersize = 25;
        public static Vector3[] Speeds = new Vector3[buffersize];

        static Vector3 AverageVelocity = Vector3.zero;

        public static void AddCurrentVelocity(Vector3 velocity)
        {
            AverageVelocity -= Speeds[index] / buffersize;

            Speeds[index] = velocity;

            AverageVelocity += velocity / buffersize;

            index = (index + 1) % buffersize;
        }

    }
}