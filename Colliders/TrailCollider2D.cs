using System.Collections.Generic;
using UnityEngine;

// Stolen from: https://www.youtube.com/watch?v=iestv-YP5CA
// https://pastebin.com/5y48DsFj

// Further edited by laserbean. 

namespace Laserbean.Colliders
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailCollider2D : MonoBehaviour
    {
        TrailRenderer myTrail;
        EdgeCollider2D myCollider;

        [SerializeField] GameObject colliderObject;

        static List<EdgeCollider2D> unusedColliders = new List<EdgeCollider2D>();

        void Awake()
        {
            myTrail = this.GetComponent<TrailRenderer>();
            myCollider = GetValidCollider();
        }

        void Update()
        {
            SetColliderPointsFromTrail(myTrail, myCollider);
        }

        //Gets from unused pool or creates one if none in pool
        EdgeCollider2D GetValidCollider()
        {
            EdgeCollider2D validCollider;
            if (unusedColliders.Count > 0) {
                validCollider = unusedColliders[0];
                validCollider.enabled = true;
                unusedColliders.RemoveAt(0);
            } else {
                if (colliderObject != null) {
                    validCollider = colliderObject.GetComponent<EdgeCollider2D>();
                    validCollider ??= colliderObject.AddComponent<EdgeCollider2D>();
                    colliderObject.transform.SetParent(null);
                } else {
                    validCollider = new GameObject("TrailCollider", typeof(EdgeCollider2D)).GetComponent<EdgeCollider2D>();
                }
            }
            return validCollider;
        }

        void SetColliderPointsFromTrail(TrailRenderer trail, EdgeCollider2D collider)
        {
            List<Vector2> points = new List<Vector2>();
            //avoid having default points at (-.5,0),(.5,0)
            if (trail.positionCount == 0) {
                points.Add(transform.position);
                points.Add(transform.position);
            } else for (int position = 0; position < trail.positionCount; position++) {
                    //ignores z axis when translating vector3 to vector2
                    points.Add(trail.GetPosition(position));
                }
            collider.SetPoints(points);
        }

        void OnDestroy()
        {
            if (myCollider != null) {
                myCollider.enabled = false;
                unusedColliders.Add(myCollider);
            }
        }
    }
}