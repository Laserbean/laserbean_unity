using System.Collections;
using System.Collections.Generic;
using Laserbean.Colliders;
using Laserbean.General;
using UnityEngine;


namespace Laserbean.Colliders.Hitbox2d
{

    public static class HitboxCreator
    {
        public static GameObject CreateHitbox(HitboxShapeData hitboxShapeData, RigidbodyInfo rigidbodyInfo)
        {

            var gameObject = new GameObject("new hitbox");

            gameObject.AddComponent<Rigidbody2D>();
            gameObject.AddComponent<SpriteRenderer>();


            // var hitboxthing = gameObject.AddComponent<HitboxControllerNew>(); 
            // // hitboxthing.SetupCollider(hitboxShapeData, istrigger:true);

            // if (gameObject.GetComponent<CustomTag>() == null) {
            //     gameObject.AddComponent<CustomTag>(); 
            // }

            // gameObject.GetComponent<CustomTag>().AddTag(Constants.TAG_HITBOX);
            // gameObject.tag = Constants.TAG_HITBOX;


            CreateColliders(ref gameObject, hitboxShapeData, istrigger: true);
            SetupRigidbody(ref gameObject, rigidbodyInfo);
            return gameObject;
        }

        public static void CreateColliders(ref GameObject cur_gameObject, HitboxShapeData hitboxshape, bool istrigger)
        {

            switch (hitboxshape.shape) {
                case HitboxShape.Rectangle:
                    var boxCollider2D = cur_gameObject.AddComponent<BoxCollider2D>();
                    boxCollider2D.offset = hitboxshape.offset;
                    boxCollider2D.size = hitboxshape.size;
                    boxCollider2D.isTrigger = istrigger;

                    boxCollider2D.enabled = true;
                    break;
                case HitboxShape.Circle:
                    var circleCollider2D = cur_gameObject.AddComponent<CircleCollider2D>();
                    circleCollider2D.offset = hitboxshape.offset;
                    circleCollider2D.radius = hitboxshape.size[0];
                    circleCollider2D.isTrigger = istrigger;

                    circleCollider2D.enabled = true;
                    break;
                case HitboxShape.Sector:
                    var polygonCollider2D = cur_gameObject.AddComponent<PolygonCollider2D>();
                    polygonCollider2D.GenerateSectorCollider(hitboxshape.size[1], 90f - hitboxshape.size[1] / 2, hitboxshape.size[0], hitboxshape.size[0] / 10, 4);
                    polygonCollider2D.offset = hitboxshape.offset;
                    polygonCollider2D.isTrigger = istrigger;
                    polygonCollider2D.enabled = true;
                    break;
                default:
                    Debug.LogError("Is this thing actually here");
                    break;

            }
        }

        public static void SetupRigidbody(ref GameObject go, RigidbodyInfo rigidbodyInfo)
        {
            var rgbd2d = go.GetComponent<Rigidbody2D>();
            rgbd2d.gravityScale = rigidbodyInfo.gravity_scale;
            rgbd2d.mass = rigidbodyInfo.mass;
            rgbd2d.drag = rigidbodyInfo.linear_drag;
            rgbd2d.freezeRotation = rigidbodyInfo.freeze_rotation;
        }

        public static void DrawRectangle(HitboxShapeData hitboxshape, Transform transform)
        {
            var offset = hitboxshape.offset;
            var size = hitboxshape.size;
            Gizmos.color = Color.red; // You can set the gizmo color to whatever you like.

            Vector3 center = transform.position + new Vector3(offset.x, offset.y, 0);
            Vector3 halfSize = new(size.x * 0.5f, size.y * 0.5f, 0);

            // Draw the rectangle using Gizmos.DrawWireCube
            Gizmos.DrawWireCube(center, halfSize * 2);
        }

        public static void DrawCircle(HitboxShapeData hitboxshape, Transform transform)
        {
            var offset = hitboxshape.offset;
            var radius = hitboxshape.size.x;

            Gizmos.color = Color.blue; // You can change the gizmo color as you like.

            Vector3 center = transform.position + new Vector3(offset.x, offset.y, 0);

            // Draw the circle using Gizmos.DrawWireSphere
            Gizmos.DrawWireSphere(center, radius);
        }

        public static void DrawSector(HitboxShapeData hitboxshape, Transform transform)
        {
            var offset = hitboxshape.offset;


            float angle = hitboxshape.size[1];
            float angleOffset = 90f - hitboxshape.size[1] / 2;
            float outerRadius = hitboxshape.size[0];
            float innerRadius = hitboxshape.size[0] / 10;
            // int numPoints = 4;

            var minAngle = 0; 
            Gizmos.color = Color.green; // You can change the gizmo color as you like.

            Vector3 center = transform.position + new Vector3(offset.x, offset.y, 0);

            // Calculate the start and end points of the sector
            Vector3 startPoint = Quaternion.Euler(0, 0, angleOffset) * Vector3.right * outerRadius;
            Vector3 endPoint = Quaternion.Euler(0, 0, angle + angleOffset) * Vector3.right * outerRadius;

            // Draw the outer sector using Gizmos.DrawRay
            Gizmos.DrawRay(center, startPoint);
            Gizmos.DrawRay(center, endPoint);

            // Calculate the start and end points of the inner sector
            Vector3 innerStartPoint = Quaternion.Euler(0, 0, angleOffset) * Vector3.right * innerRadius;
            Vector3 innerEndPoint = Quaternion.Euler(0, 0, angle + angleOffset) * Vector3.right * innerRadius;

            // Draw the inner sector using Gizmos.DrawRay
            Gizmos.DrawRay(center, innerStartPoint);
            Gizmos.DrawRay(center, innerEndPoint);

            // Draw the arcs connecting the outer and inner radii
            float step = 1.0f; // Resolution of the arc, adjust as needed
            for (float a = minAngle + angleOffset; a <= minAngle + angle + angleOffset; a += step) {
                Vector3 startOuter = Quaternion.Euler(0, 0, a) * Vector3.right * outerRadius;
                Vector3 endOuter = Quaternion.Euler(0, 0, a + step) * Vector3.right * outerRadius;
                Gizmos.DrawLine(center + startOuter, center + endOuter);

                Vector3 startInner = Quaternion.Euler(0, 0, a) * Vector3.right * innerRadius;
                Vector3 endInner = Quaternion.Euler(0, 0, a + step) * Vector3.right * innerRadius;
                Gizmos.DrawLine(center + startInner, center + endInner);

                // Connect the outer and inner radii
                Gizmos.DrawLine(center + startOuter, center + startInner);
            }
        }

    }

}
