using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General;

namespace Laserbean.General.MathExtra
{

    public static class MathL
    {

        public static int Mod(int x, int m)
        {
            // return (x%m + m)%m;
            int r = x % m;
            return r < 0 ? r + m : r;
        }


        public static Vector2Int Mod(Vector2Int vec, int m)
        {
            vec.x = Mod(vec.x, m);
            vec.y = Mod(vec.y, m);

            return vec;
        }

        public static Vector2Int GetChunkPosition(Vector2Int vec, int GridSize)
        {
            return vec - Mod(vec, GridSize);
        }


        public static float Snap(this float x, float snapAngle)
        {
            if (snapAngle == 0)
            {
                return x;
            }
            return Mathf.Round(x / snapAngle) * snapAngle;
        }


    }

}