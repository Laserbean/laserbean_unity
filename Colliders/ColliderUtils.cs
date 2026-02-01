using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Laserbean.Colliders
{
    public static class ColliderUtils
    {
        public static List<Collider2D> GetOverlapCollider2D(Collider2D collider)
        {
            List<Collider2D> colliders = new();
            ContactFilter2D filter = ContactFilter2D.noFilter;
            Physics2D.OverlapCollider(collider, filter, colliders);
            return colliders;
        }

        public static List<Collider2D> GetOverlapCollider2D(Collider2D collider, ContactFilter2D filter)
        {
            List<Collider2D> colliders = new();
            Physics2D.OverlapCollider(collider, filter, colliders);
            return colliders;
        }


    }
}

