using System;
using Cysharp.Threading.Tasks;
using Laserbean.General;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

//https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask

/// <summary>
/// TEMPLATE for the Game initiator model from PracticAPI
/// </summary>

public class GameInitiator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject _player;
    [SerializeField] Camera _mainCamera;
    [SerializeField] GameObject _border;
    [SerializeField] GameObject _globalLight2d;
    [SerializeField] ObjectPooler _objectPooler;
    [SerializeField] GameObject _canvas;

    //references
    // [SerializeField] InputActionAsset _inputactions; 

    [SerializeField] GameObject _eventsystem;

    async void Start()
    {
        BindObjects();
        //loadingscreen.show()
        await IntialializeObjects();
        await CreateObjects();

        PrepareGame();
        //loading screen hide
        await BeginGame();
    }

    private void BindObjects()
    {
        _mainCamera = Instantiate(_mainCamera);
        // _eventsystem = Instantiate(_eventsystem); 

        _border = Instantiate(_border);
        _globalLight2d = Instantiate(_globalLight2d);
        _objectPooler = Instantiate(_objectPooler);
        _canvas = Instantiate(_canvas);
    }

    private async UniTask CreateObjects()
    {
        await UniTask.Delay(0);
        // _eventsystem.GetComponent<InputSystemUIInputModule>().actionsAsset = _inputactions;

        _player = Instantiate(_player);
        var playerinput = _player.GetComponent<PlayerInput>();

        playerinput.uiInputModule = _eventsystem.GetComponent<InputSystemUIInputModule>();
        // playerinput.GetComponent<ReferenceSetter>().SetCamera(_mainCamera); 

    }

    async UniTask IntialializeObjects()
    {
        // await analytics?S
        // _gameInputSystem.Enable();
        await UniTask.Delay(0);
    }


    private void PrepareGame()
    {
        // move player to random pos?
    }


    private async UniTask BeginGame()
    {
        //show level animation
        //show enemies
        await UniTask.Delay(0);
    }

}
