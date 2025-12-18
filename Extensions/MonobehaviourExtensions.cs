using System;
using UnityEngine;
using System.Linq;


using System.Collections.Generic;
using UnityEditor;


namespace Laserbean.General
{
    public static class InterfaceExtensions
    {
        /// <summary>
        /// Returns all monobehaviours (casted to T)
        /// </summary>
        /// <typeparam name="T">interface type</typeparam>
        /// <param name="gObj"></param>
        /// <returns></returns>
        public static T[] GetInterfaces<T>(this GameObject gObj)
        {
            if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
            var mObjs = gObj.GetComponents<MonoBehaviour>();

            return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T)(object)a).ToArray();
        }

        /// <summary>
        /// Returns the first monobehaviour that is of the interface type (casted to T)
        /// </summary>
        /// <typeparam name="T">Interface type</typeparam>
        /// <param name="gObj"></param>
        /// <returns></returns>
        public static T GetInterface<T>(this GameObject gObj)
        {
            if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
            return gObj.GetInterfaces<T>().FirstOrDefault();
        }

        /// <summary>
        /// Returns the first instance of the monobehaviour that is of the interface type T (casted to T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gObj"></param>
        /// <returns></returns>
        public static T GetInterfaceInChildren<T>(this GameObject gObj)
        {
            if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");
            return gObj.GetInterfacesInChildren<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets all monobehaviours in children that implement the interface of type T (casted to T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gObj"></param>
        /// <returns></returns>
        public static T[] GetInterfacesInChildren<T>(this GameObject gObj)
        {
            if (!typeof(T).IsInterface) throw new SystemException("Specified type is not an interface!");

            var mObjs = gObj.GetComponentsInChildren<MonoBehaviour>();

            return (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(T)) select (T)(object)a).ToArray();
        }
    }

    public static class MoreExtensions
    {
        // ///<summary>Does this depending on if it containes the pooled object tag </summary>
        // public static void DestoyOrDeactivate(this GameObject go) {
        //     if (go.HasTag(Constants.TAG_POOLED)) {
        //         go.SetActive(false); 
        //     } else {
        //         GameObject.Destroy(go); 
        //     }
        // }

        // Constants

        static System.Collections.IEnumerator DoAnimation(SpriteRenderer spriteRenderer, List<Sprite> sprites, float frame_duration)
        {
            int nnnn = sprites.Count;
            for (int j = 0; j < nnnn; j++)
            {
                spriteRenderer.sprite = sprites[j];
                yield return new WaitForSeconds(frame_duration);
            }

        }


        // <summary> 
        // NOTE: Start with Coroutine
        // </summary>
        public static System.Collections.IEnumerator DoAnimationTotalTime(this SpriteRenderer spriteRenderer, List<Sprite> sprites, float total_time)
        {
            yield return DoAnimation(spriteRenderer, sprites, total_time / sprites.Count);
        }

        public static System.Collections.IEnumerator DoAnimationFrameDuration(this SpriteRenderer spriteRenderer, List<Sprite> sprites, float frame_duration)
        {
            yield return DoAnimation(spriteRenderer, sprites, frame_duration);
        }


        public static System.Collections.IEnumerator PlayAnimationRepeating(this SpriteRenderer spriteRenderer, List<Sprite> sprites, float duration, float frame_duration)
        {
            Debug.Log("Start PlayAnimationCoroutine");

            float time = 0f;
            while (time < duration)
            {
                foreach (Sprite frame in sprites)
                {
                    spriteRenderer.sprite = frame;
                    yield return new WaitForSeconds(frame_duration);
                    time += frame_duration;

                    if (time >= duration)
                    {
                        break;
                    }
                }
            }
        }

        public static bool ContainsParam(this Animator _Anim, string _ParamName)
        {
            foreach (AnimatorControllerParameter param in _Anim.parameters)
            {
                if (param.name == _ParamName) return true;
            }
            return false;
        }
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            return component != null ? component : gameObject.AddComponent<T>();
        }
        
    }



    // public static class PrefabExtensions {
    //     public static void InstantiateAndReplace<T>(this T monobehaviour) where T : UnityEngine.Object
    //     {
    //         monobehaviour = UnityEngine.Object.Instantiate(monobehaviour);
    //     }
    // }


}