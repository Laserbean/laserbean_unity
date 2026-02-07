
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General
{


    public static class VectorExtensions
    {

        public static float CalculateAbsoluteAngle(float angle1, float angle2)
        {
            angle1 = (angle1 + 360) % 360;
            angle2 = (angle2 + 360) % 360;

            float angleDifference = Mathf.Abs(angle1 - angle2);

            if (angleDifference > 180)
            {
                // Adjust for angles that wrap around
                angleDifference = 360 - angleDifference;
            }

            return angleDifference;
        }

        public static float CalculateAngle(Vector3 from, Vector3 to)
        {
            return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
        }

        public static float VectorToAngle(this Vector2 vect)
        {
            return Vector2.SignedAngle(new Vector2(0, 1), vect.normalized);
        }

        public static float VectorToAngle(this Vector3 vect)
        {
            return Vector2.SignedAngle(new Vector2(0, 1), vect.normalized);
        }


        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }

        public static Vector2Int Rotate(this Vector2Int v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (int)((cos * tx) - (sin * ty));
            v.y = (int)((sin * tx) + (cos * ty));
            return v;
        }

        public static Vector3 Rotate(this Vector3 v, float degrees)
        {
            //about z cause i'm doing 2d lol
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }

        public static Vector3 Rotate(this Vector3 v, float degrees, CardinalAxis axis)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            switch (axis)
            {
                case CardinalAxis.X:
                    RotateAroundX(ref v, sin, cos);
                    break;

                case CardinalAxis.Y:
                    RotateAroundY(ref v, sin, cos);
                    break;

                case CardinalAxis.Z:
                    RotateAroundZ(ref v, sin, cos);
                    break;

                default:
                    // Just in case you try something weird
                    break;
            }

            return v;
        }

        private static void RotateAroundX(ref Vector3 v, float sin, float cos)
        {
            float ty = v.y;
            v.y = (cos * v.y) - (sin * v.z);
            v.z = (sin * ty) + (cos * v.z);
        }

        private static void RotateAroundY(ref Vector3 v, float sin, float cos)
        {
            float tx = v.x;
            v.x = (cos * v.x) + (sin * v.z);
            v.z = -(sin * tx) + (cos * v.z);
        }

        private static void RotateAroundZ(ref Vector3 v, float sin, float cos)
        {
            float tx = v.x;
            v.x = (cos * v.x) - (sin * v.y);
            v.y = (sin * tx) + (cos * v.y);
        }


        public static Vector2 Snap(this Vector2 v)
        {

            v.x = Mathf.Round(v.x);
            v.y = Mathf.Round(v.y);
            return v;
        }

        public static Vector3 Snap(this Vector3 v)
        {

            v.x = Mathf.Round(v.x);
            v.y = Mathf.Round(v.y);
            v.z = Mathf.Round(v.z);
            return v;
        }


        #region  Fast Conversion

        // To Vector2

        public static Vector2 ToVector2(this Vector2Int v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector2 ToVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector2 ToVector2(this Vector3Int v)
        {
            return new Vector2(v.x, v.y);
        }



        // To Vector2Int

        public static Vector2Int ToVector2Int(this Vector2 v)
        {
            return new Vector2Int((int)v.x, (int)v.y);
        }

        public static Vector2Int ToVector2Int(this Vector3 v)
        {
            return new Vector2Int((int)v.x, (int)v.y);
        }

        public static Vector2Int ToVector2Int(this Vector3Int v)
        {
            return new Vector2Int((int)v.x, (int)v.y);
        }

        // To Vector3

        public static Vector3 ToVector3(this Vector2 v)
        {
            return new Vector3(v.x, v.y);
        }

        public static Vector3 ToVector3(this Vector2Int v)
        {
            return new Vector3(v.x, v.y);
        }

        public static Vector3 ToVector3(this Vector3Int v)
        {
            return new Vector3(v.x, v.y);
        }

        // To Vector3Int

        public static Vector3Int ToVector3Int(this Vector2 v)
        {
            return new Vector3Int((int)v.x, (int)v.y);
        }

        public static Vector3Int ToVector3Int(this Vector2Int v)
        {
            return new Vector3Int(v.x, v.y);
        }

        public static Vector3Int ToVector3Int(this Vector3 v)
        {
            return new Vector3Int((int)v.x, (int)v.y);
        }

        //Fast Rounding Conversions; 

        public static Vector3Int ToVector3IntRound(this Vector2 v)
        {
            return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }

        public static Vector3Int ToVector3IntRound(this Vector3 v)
        {
            return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        }

        public static Vector2Int ToVector2IntRound(this Vector2 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }

        public static Vector2Int ToVector2IntRound(this Vector3 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }

        // Floor
        public static Vector3Int ToVector3IntFloor(this Vector2 v)
        {
            return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        }

        public static Vector3Int ToVector3IntFloor(this Vector3 v)
        {
            return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
        }

        public static Vector2Int ToVector2IntFloor(this Vector2 v)
        {
            return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        }

        public static Vector2Int ToVector2IntFloor(this Vector3 v)
        {
            return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        }

        public static Vector3 ToVector3Floor(this Vector2 v)
        {
            return new Vector3(Mathf.Floor(v.x), Mathf.Floor(v.y));
        }

        public static Vector3 ToVector3Floor(this Vector3 v)
        {
            return new Vector3(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z));
        }

        public static Vector2 ToVector2Floor(this Vector2 v)
        {
            return new Vector2(Mathf.Floor(v.x), Mathf.Floor(v.y));
        }

        public static Vector2 ToVector2Floor(this Vector3 v)
        {
            return new Vector2(Mathf.Floor(v.x), Mathf.Floor(v.y));
        }


        // Ceil 
        public static Vector3Int ToVector3IntCeil(this Vector2 v)
        {
            return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
        }

        public static Vector3Int ToVector3IntCeil(this Vector3 v)
        {
            return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
        }

        public static Vector2Int ToVector2IntCeil(this Vector2 v)
        {
            return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
        }

        public static Vector2Int ToVector2IntCeil(this Vector3 v)
        {
            return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
        }


        // Ceil 
        public static Vector3 ToVector3Ceil(this Vector2 v)
        {
            return new Vector3(Mathf.Ceil(v.x), Mathf.Ceil(v.y));
        }

        public static Vector3 ToVector3Ceil(this Vector3 v)
        {
            return new Vector3(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z));
        }

        public static Vector2 ToVector2Ceil(this Vector2 v)
        {
            return new Vector2(Mathf.Ceil(v.x), Mathf.Ceil(v.y));
        }

        public static Vector2 ToVector2Ceil(this Vector3 v)
        {
            return new Vector2(Mathf.Ceil(v.x), Mathf.Ceil(v.y));
        }


        #endregion

        public static void Decompose(Vector2 v, Vector2 reference, out Vector2 parallel, out Vector2 perpendicular)
        {
            Vector2 n = reference.normalized;
            parallel = Vector2.Dot(v, n) * n;
            perpendicular = v - parallel;
        }

        // Overload for Vector3
        public static void Decompose(Vector3 v, Vector3 reference, out Vector3 parallel, out Vector3 perpendicular)
        {
            Vector3 n = reference.normalized;
            parallel = Vector3.Dot(v, n) * n;
            perpendicular = v - parallel;
        }


        public static Vector3 Modify(this Vector3 v, float value, CardinalAxis axis)
        {
            switch (axis)
            {
                case CardinalAxis.X:
                    return new(value, v.y, v.z);
                case CardinalAxis.Y:
                    return new(v.x, value, v.z);
                case CardinalAxis.Z:
                    return new(v.x, v.y, value);
                default:
                    return v;
            }
        }


        public static Vector3 Average(this List<Vector3> Targets)
        {
            Vector3 averagepos = Vector3.zero;
            for (int i = 0; i < Targets.Count; i++)
            {
                averagepos += Targets[i];
            }
            averagepos /= Targets.Count;
            return averagepos;
        }


        public static Vector3 WeightedAverage(this List<Vector3> Targets, List<float> posweights)
        {
            Vector3 averagepos = Vector3.zero;
            float weights = 0f;
            for (int i = 0; i < Targets.Count; i++)
            {
                averagepos += Targets[i] * posweights[i];
                weights += posweights[i];
            }
            averagepos /= weights;
            return averagepos;
        }
    }


    public enum CardinalAxis
    {
        X,
        Y,
        Z
    }


    public static class QuaternionExtension
    {
        // Note: This works well if the quaternions are relatively close together.
        public static Quaternion Average(this List<Quaternion> quaternions)
        {
            if (quaternions.Count == 0)
            {
                return Quaternion.identity;
            }

            Vector4 cumulative = Vector4.zero;
            Quaternion firstRotation = quaternions[0];

            // Ensure all quaternions are on the same "side" of the 4D sphere
            foreach (var nextRotation in quaternions)
            {
                // If the dot product is negative, the quaternions are pointing in opposite directions, 
                // so we reverse the sign of the nextRotation components to average correctly.
                if (Vector4.Dot(new Vector4(nextRotation.x, nextRotation.y, nextRotation.z, nextRotation.w),
                                new Vector4(firstRotation.x, firstRotation.y, firstRotation.z, firstRotation.w)) < 0)
                {
                    cumulative += new Vector4(-nextRotation.x, -nextRotation.y, -nextRotation.z, -nextRotation.w);
                }
                else
                {
                    cumulative += new Vector4(nextRotation.x, nextRotation.y, nextRotation.z, nextRotation.w);
                }
            }

            // Average the components
            cumulative /= quaternions.Count;

            // If the cumulative is (near) zero, fall back to the first rotation
            if (cumulative.sqrMagnitude < 1e-8f)
            {
                return firstRotation;
            }

            // Normalize the vector and return as a Quaternion
            return new Quaternion(cumulative.x, cumulative.y, cumulative.z, cumulative.w).normalized;
        }

        // Weighted average. Weights must be same length as quaternions; zero/negative weights are allowed
        // (they will influence the result accordingly). If total weight is zero, returns Quaternion.identity.
        public static Quaternion WeightedAverage(this List<Quaternion> quaternions, List<float> weights)
        {
            if (quaternions == null) throw new ArgumentNullException(nameof(quaternions));
            if (weights == null) throw new ArgumentNullException(nameof(weights));
            if (quaternions.Count == 0)
            {
                return Quaternion.identity;
            }
            if (weights.Count != quaternions.Count)
            {
                throw new ArgumentException("Weights length must match quaternions length.", nameof(weights));
            }

            Vector4 cumulative = Vector4.zero;
            Quaternion firstRotation = quaternions[0];

            float totalWeight = 0f;

            for (int i = 0; i < quaternions.Count; i++)
            {
                var q = quaternions[i];
                float w = weights[i];

                Vector4 v = new Vector4(q.x, q.y, q.z, q.w);

                // Ensure consistent hemisphere
                if (Vector4.Dot(v, new Vector4(firstRotation.x, firstRotation.y, firstRotation.z, firstRotation.w)) < 0f)
                {
                    v = -v;
                }

                cumulative += v * w;
                totalWeight += w;
            }

            if (Mathf.Approximately(totalWeight, 0f))
            {
                return Quaternion.identity;
            }

            cumulative /= totalWeight;

            if (cumulative.sqrMagnitude < 1e-8f)
            {
                return firstRotation;
            }

            return new Quaternion(cumulative.x, cumulative.y, cumulative.z, cumulative.w).normalized;
        }
    }

}