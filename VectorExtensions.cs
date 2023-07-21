
using UnityEngine;

public static class VectorExtensions
{


    public static float CalculateAngle(Vector3 from, Vector3 to) {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

    public static Vector2 Rotate(this Vector2 v, float degrees){
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public static Vector2Int Rotate(this Vector2Int v, float degrees){
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
        float tx = v.x;
        float ty = v.y;
        v.x = (int)((cos * tx) - (sin * ty));
        v.y = (int)((sin * tx) + (cos * ty));
        return v;
    }

    public static Vector3 Rotate(this Vector3 v, float degrees){
        //about z cause i'm doing 2d lol
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public static Vector2 Snap(this Vector2 v){

        v.x = Mathf.Round(v.x);
        v.y = Mathf.Round(v.y);
        return v;
    }

    public static Vector3 Snap(this Vector3 v){

        v.x = Mathf.Round(v.x);
        v.y = Mathf.Round(v.y);
        v.z = Mathf.Round(v.z);
        return v;
    }

    public static Vector2Int ToVector2Int(this Vector2 v){
        //about z cause i'm doing 2d lol
         
        return new Vector2Int((int)v.x, (int) v.y); 
    }

    public static Vector3 ToVector3(this Vector2Int v){
        //about z cause i'm doing 2d lol
         
        return new Vector3(v.x, v.y); 
    }

    public static Vector3 ToVector3(this Vector2 v){
        //about z cause i'm doing 2d lol
         
        return new Vector3(v.x, v.y); 
    }

    public static Vector2 ToVector2(this Vector2Int v){
        //about z cause i'm doing 2d lol
         
        return new Vector2(v.x, v.y); 
    }

    public static Vector2 ToVector2(this Vector3 v){
        //about z cause i'm doing 2d lol
         
        return new Vector2(v.x, v.y); 
    }



    public static Vector2Int ToVector2Int(this Vector3 v){
        //about z cause i'm doing 2d lol
         
        return new Vector2Int((int)v.x, (int) v.y); 
    }

    public static Vector2Int ToVector2Int(this Vector3Int v){
        //about z cause i'm doing 2d lol
        return new Vector2Int((int)v.x, (int) v.y); 
    }


    public static Vector2Int RoundToVector2Int(this Vector3 v){
        //about z cause i'm doing 2d lol
         
        return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y)); 
    }

    public static Vector2Int RoundToVector2Int(this Vector3Int v){
        //about z cause i'm doing 2d lol
        return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y)); 
    }

    public static Vector3Int RoundToVector3Int(this Vector3 v){
        //about z cause i'm doing 2d lol
        return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z)); 
    }

}
