
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Laserbean.Colliders.Hitbox2d {


[System.Serializable]
public struct HitboxShapeData {
    public Vector2 size; 
    public Vector2 offset; 
    public Vector2 local_position; 
    public HitboxShape shape; 

}


public enum HitboxShape {
    None,
    Rectangle,
    Circle, 
    Sector
}



[System.Serializable]
public class RigidbodyInfo {
    public bool isTrigger;
    public bool canPassWalls; 
    public float mass; 
    public float linear_drag; 
    public float gravity_scale; 
    public bool freeze_rotation;
}


}

