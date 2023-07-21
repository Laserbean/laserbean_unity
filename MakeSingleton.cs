using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSingleton : Singleton<MakeSingleton>
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    [SerializeField] bool persistant_between_scenes; 
    void Awake()
    {
        if (persistant_between_scenes) {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
