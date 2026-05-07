using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using EasyButtons;

using UnityEngine.SceneManagement;


using System;
using Laserbean.General;
using UnityEngine.Events;

public class Scene_Controller : MonoBehaviour
{


    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;

    }

    private void OnSceneLoad(Scene scene, LoadSceneMode arg1)
    {
        OnSceneLoaded?.Invoke(scene.name);
    }

    [SerializeField]
    UnityEvent<string> OnSceneLoaded;


    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }




    // public void Pause(bool paused)
    // {
    //     if (!paused)
    //     {
            
    //     }
    //     else
    //     {
    //     }
    // }



}

// public enum GameState
// {
//     MenuLoad,
//     Menu,

//     GameLoad,
//     Running,
//     Paused
// }

