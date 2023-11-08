
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

            if (angleDifference > 180) {
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

    }


}