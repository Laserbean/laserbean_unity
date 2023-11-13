using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using EasyButtons;

using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{

    private GameState curstate;

    public GameObject player = null;

    public bool isDebugPath = false;

    public string AppPath {
        get {
            if (_appPath == "") {
                if (Application.isEditor && isDebugPath) {
                    _appPath = "/unity_projects/Debug";
                }
                else {
                    _appPath = Application.persistentDataPath;
                }
            }
            return _appPath;
        }
    }

    string gamepath = "";
    public string GamePath {
        get => AppPath + "/saves/" + gamepath;
    }

    public void SetGamePath(string pth)
    {
        gamepath = pth;
    }



    string _appPath = "";

    public bool debug = true;

    private void Awake()
    {
        curstate = GameState.Menu;

    }


    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void DebugToggle(bool dee)
    {
        debug = dee;
    }



    [Button]
    public void StartGame()
    {
        curstate = GameState.Running;
        RecalculateGraph();
    }


    public void PauseGame()
    {
        curstate = GameState.Paused;
    }

    public void UnPauseGame()
    {
        curstate = GameState.Running;
    }


    public void StopGame()
    {
        curstate = GameState.Menu;
    }

    [Button]
    public void RecalculateGraph()
    {
        AstarPath.active.Scan();
    }

    public void Pause(bool paused)
    {
        if (curstate == GameState.Running && !paused) {
            curstate = GameState.Paused;
            Time.timeScale = 0f;
            // Actions.OnPause(); 
        }
        else if (curstate == GameState.Paused && paused) {
            curstate = GameState.Running;
            Time.timeScale = 1f;
            // Actions.OnPause(); 
        }
    }

    public bool IsPaused {
        get {
            return curstate == GameState.Paused;
        }
    }

    public bool IsRunning {
        get {
            return (curstate == GameState.Running) || debug;
        }
    }



}

public enum GameState
{
    Running, Paused, Menu, Intro
}

