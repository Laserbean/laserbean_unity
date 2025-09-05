using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

using System.Linq;

namespace Laserbean.General
{
    public static class GameObjectExtensions
    {

        #region CustomTag

        public static bool HasTag(this GameObject go, string tag)
        {
            CustomTag comp = go.GetComponent<CustomTag>();
            if (comp != null)
            {
                return comp.HasTag(tag);
            }
            return go.tag == tag;
        }

        ///<summary>
        ///Checks if this object has any overlapping tags with the _tags provided
        ///</summary>
        public static List<string> ContainedTags(this GameObject go, List<string> _tags)
        {
            CustomTag comp = go.GetComponent<CustomTag>();
            List<string> res = new List<string>();
            if (comp != null)
            {
                res = comp.GetTags().Intersect(_tags).ToList<string>();
            }
            return res;
        }
        public static void AddTag(this GameObject go, string tag)
        {
            CustomTag comp = go.GetComponent<CustomTag>();
            List<string> res = new List<string>();
            if (!comp.HasTag(tag))
            {
                comp.AddTag(tag);
            }
        }
        #endregion


    }
}