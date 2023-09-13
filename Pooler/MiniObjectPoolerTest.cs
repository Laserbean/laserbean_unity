using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General;
using System.Diagnostics;

public class MiniObjectPoolerTest : MonoBehaviour {
    MiniObjectPooler objectPooler; 

    [SerializeField] GameObject prefab; 

    private void Start() {
        objectPooler = new (new ObjectPoolItem(prefab, 5), transform); 
    }

    [EasyButtons.Button]
    public void Insta() {
        var go = objectPooler.GetPooledObject(); 

        go.SetActive(true);
        go.transform.position = transform.position; 

    }
}