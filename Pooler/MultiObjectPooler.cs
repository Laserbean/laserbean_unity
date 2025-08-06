using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General
{
    public class MultiObjectPooler
    {
        public List<ObjectPoolItem> itemsToPool;
        public List<List<GameObject>> pooledObjectsList;
        public List<GameObject> pooledObjects;
        private List<int> positions;

        Transform parent;

        public MultiObjectPooler(Transform _parent)
        {
            parent = _parent;

            pooledObjectsList = new List<List<GameObject>>();
            pooledObjects = new List<GameObject>();
            positions = new List<int>();


            for (int i = 0; i < itemsToPool.Count; i++)
            {
                ObjectPoolItemToPooledObject(i);
            }
        }


        public GameObject GetPooledObject(int index)
        {

            int curSize = pooledObjectsList[index].Count;
            for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
            {

                if (!pooledObjectsList[index][i % curSize].activeInHierarchy)
                {
                    positions[index] = i % curSize;
                    return pooledObjectsList[index][i % curSize];
                }
            }

            if (itemsToPool[index].shouldExpand)
            {
                GameObject obj = GameObject.Instantiate(itemsToPool[index].objectToPool);
                obj.SetActive(false);
                obj.transform.SetParent(parent);

                pooledObjectsList[index].Add(obj);
                return obj;
            }
            return null;
        }

        public List<GameObject> GetAllPooledObjects(int index)
        {
            return pooledObjectsList[index];
        }


        public int AddObject(GameObject GO, int amt = 3, bool exp = true)
        {
            ObjectPoolItem item = new(GO, amt, exp);
            int currLen = itemsToPool.Count;
            itemsToPool.Add(item);
            ObjectPoolItemToPooledObject(currLen);
            return currLen;
        }


        void ObjectPoolItemToPooledObject(int index)
        {
            ObjectPoolItem item = itemsToPool[index];

            pooledObjects = new List<GameObject>();
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = GameObject.Instantiate(item.objectToPool);
                obj.SetActive(false);
                obj.transform.SetParent(parent);
                pooledObjects.Add(obj);
            }
            pooledObjectsList.Add(pooledObjects);
            positions.Add(0);

        }

        public void DisablePooledObjects(int index)
        {
            foreach (var go in pooledObjectsList[index])
            {
                go.SetActive(false);
            }
        }
    }


}