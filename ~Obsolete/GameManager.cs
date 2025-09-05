// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// using EasyButtons;

// using UnityEngine.SceneManagement;


// using System;
// using Laserbean.General;
// using UnityEngine.Events;

// public class GameManager : PersistentSingleton<GameManager>
// {

//     [SerializeField] int TargetFrameRate = 30;
//     private GameState curstate;
//     public GameObject player = null;

//     public bool isDebugPath = false;
//     public bool debug = true;


//     public bool IsPaused {
//         get {
//             return curstate == GameState.Paused;
//         }
//     }

//     public bool IsRunning {
//         get {
//             return (curstate == GameState.Running) || debug;
//         }
//     }


//     #region GamePath

//     public string AppPath {
//         get {
//             if (_appPath == "") {
//                 if (Application.isEditor && isDebugPath) {
//                     _appPath = "/unity_projects/Debug";
//                 } else {
//                     _appPath = Application.persistentDataPath;
//                 }
//             }
//             return _appPath;
//         }
//     }

//     string gamepath = "";
//     public string GamePath {
//         get => AppPath + "/saves/" + gamepath;
//     }

//     public void SetGamePath(string pth)
//     {
//         gamepath = pth;
//     }



//     string _appPath = "";

//     #endregion


//     public Func<IEnumerator> OnStartMenuLoadHandler;
//     public Action OnFinishMenuLoad;

//     public Func<IEnumerator> OnNewGameHandler;
//     // public Action OnFinishNewGame;

//     public Func<IEnumerator> OnGameLoadHandler;
//     public event Action OnFinishGameLoad;
//     public event Action OnStartGame;
//     public event Action OnStopGame;
//     public event Action OnPauseGame;
//     public event Action OnUnpauseGame;
//     public event Action OnSaveGame;
//     public event Action<string> OnSceneLoaded;

//     protected override void Awake()
//     {
//         base.Awake(); 
//         SceneManager.sceneLoaded += OnSceneLoad;
//     }

//     private void OnDestroy()
//     {
//         OnStartMenuLoadHandler = null;
//         OnFinishMenuLoad = null;
//         OnNewGameHandler = null;
//         OnGameLoadHandler = null;
//         OnFinishGameLoad = null;
//         OnStartGame = null;
//         OnStopGame = null;
//         OnPauseGame = null;
//         OnUnpauseGame = null;
//         OnSaveGame = null;
//         SceneManager.sceneLoaded -= OnSceneLoad;

//     }

//     private void OnSceneLoad(Scene scene, LoadSceneMode arg1)
//     {
//         OnSceneLoaded?.Invoke(scene.name); 
//     }


//     private IEnumerator Start()
//     {
//         Application.targetFrameRate = TargetFrameRate;
//         yield return new WaitForSeconds(1f);
//         yield return StartCoroutine(MenuSetup());
//     }


//     IEnumerator MenuSetup()
//     {
//         Debug.Log("Start setup".DebugColor(Color.blue));
//         yield return OnStartMenuLoadHandler?.InvokeAndWait(gameObject);
//         Debug.Log("MenuSetup Finished".DebugColor(Color.green));
//         OnFinishMenuLoad?.Invoke();
//     }

//     IEnumerator GameSetup()
//     {
//         Debug.Log("Start Game Load".DebugColor(Color.blue));
//         yield return OnGameLoadHandler?.InvokeAndWait(gameObject);
//         Debug.Log("Game Loaded".DebugColor(Color.green));
//         OnFinishGameLoad?.Invoke();
//     }


//     IEnumerator NewGameSetup()
//     {
//         Debug.Log("Start New Game".DebugColor(Color.blue));
//         yield return OnNewGameHandler?.InvokeAndWait(gameObject);
//         Debug.Log("Game Created".DebugColor(Color.green));
//         OnFinishGameLoad?.Invoke();
//     }


//     public void LoadScene(string name)
//     {
//         SceneManager.LoadScene(name);
//     }

//     public void DebugToggle(bool dee)
//     {
//         debug = dee;
//     }


//     [Button]
//     public void StartLoadGame()
//     {
//         StartCoroutine(GameSetup());
//     }

//     [Button]
//     public void StartNewGame()
//     {
//         StartCoroutine(NewGameSetup());
//     }

//     [Button]
//     public void SaveGame()
//     {
//         OnSaveGame?.Invoke();
//     }


//     [Button]
//     public void StartGame()
//     {
//         Debug.Log("StartGame");
//         curstate = GameState.Running;
//         // RecalculateGraph();
//         OnStartGame?.Invoke();
//     }

//     public void PauseGame()
//     {
//         if (curstate == GameState.Running) {
//             curstate = GameState.Paused;
//             Time.timeScale = 0f;
//             OnPauseGame?.Invoke();
//             Debug.Log("Pause".Color(Color.red)); 
//         }
//     }

//     public void UnPauseGame()
//     {
//         if (curstate == GameState.Paused) {
//             curstate = GameState.Running;
//             Time.timeScale = 1f;
//             OnUnpauseGame?.Invoke();
//             Debug.Log("Unpause".Color(Color.red)); 

//         }
//     }

//     public void StopGame()
//     {
//         curstate = GameState.Menu;
//         OnStopGame?.Invoke();
//     }

//     // [Button]
//     // public void RecalculateGraph()
//     // {
//     //     AstarPath.active?.Scan();
//     // }

//     public void Pause(bool paused)
//     {
//         if (!paused) {
//             PauseGame();
//         } else {
//             UnPauseGame();
//         }
//     }



// }

// public enum GameState
// {
//     MenuLoad,
//     Menu,

//     GameLoad,
//     Running,
//     Paused
// }

