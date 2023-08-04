using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General
{


public class MiniObjectPooler {

    Transform parent; 
	public ObjectPoolItem itemToPool;

	public List<GameObject> pooledObjectList = new List<GameObject>(); 



    public MiniObjectPooler(ObjectPoolItem item, Transform _parent) { 
        parent = _parent; 
        itemToPool = item; 
        AddPooledObject();
    }

    public GameObject GetPooledObject() {

        foreach(var go in pooledObjectList) {
            if (go.activeInHierarchy) {
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
        GameObject go = MonoBehaviour.Instantiate(itemToPool.objectToPool, Vector3.zero, Quaternion.identity); 
        go.transform.SetParent(parent);
        go.SetActive(false);

        go.transform.localPosition = Vector3.zero; 
        go.transform.localRotation = Quaternion.identity; 

        

        pooledObjectList.Add(go);
        return go;
    }

    
}


}