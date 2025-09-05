using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General

{
    public class Pooler : MonoBehaviour
    {
        MultiObjectPooler objectPooler; 

        protected void Awake()
        {
            objectPooler = new MultiObjectPooler(transform);

        }


        public GameObject GetPooledObject(int index)
        {
            return objectPooler.GetPooledObject(index);
        }

        public List<GameObject> GetAllPooledObjects(int index)
        {
            return objectPooler.GetAllPooledObjects(index);
        }


        public int AddObject(GameObject GO, int amt = 3, bool exp = true)
        {
            return objectPooler.AddObject(GO, amt, exp);
        }

        public void DisableAllPooledObjects(int index)
        {
            objectPooler.DisablePooledObjects(index);
        }
    }


}