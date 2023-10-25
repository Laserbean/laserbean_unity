using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General
{


public class MiniObjectPooler {

    Transform Parent; 
	public ObjectPoolItem itemToPool;

	public List<GameObject> pooledObjectList = new (); 



    public MiniObjectPooler(ObjectPoolItem item, Transform _parent) { 
        Parent = _parent; 
        itemToPool = item; 

        if (item != null)
            AddPooledObject();
    }

    public void SetPoolItem(ObjectPoolItem item, Transform _parent) {
        try {
            Object.Destroy(itemToPool.objectToPool);
        } catch {}

        Parent = _parent; 
        itemToPool = item; 
        AddPooledObject();
    }

    public GameObject GetPooledObject() {

        foreach(var go in pooledObjectList) {
            if (!go.activeInHierarchy) {
                return go; 
            }
        }

        if (pooledObjectList.Count < itemToPool.amountToPool) {
            return AddPooledObject();
        }
        if (itemToPool.shouldExpand) {
            return AddPooledObject();
        }
        return null; 
    }


    public GameObject AddPooledObject() {
        GameObject go = Object.Instantiate(itemToPool.objectToPool, Vector3.zero, Quaternion.identity); 
        go.transform.SetParent(Parent);
        go.SetActive(false);

        go.transform.localPosition = Vector3.zero; 
        go.transform.localRotation = Quaternion.identity; 

        

        pooledObjectList.Add(go);
        return go;
    }


    public void DestroyAllPooledObjects() {
        for(int i = pooledObjectList.Count-1; i >= 0; i--) {
            Object.Destroy(pooledObjectList[i]); 
        }
        pooledObjectList.Clear(); 
    }
    
}


}