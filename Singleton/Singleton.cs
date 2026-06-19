using UnityEngine;

// copied from: https://github.com/adammyhre/Unity-Utils/blob/master/Assets/_Project/Scripts/Singleton/Singleton.cs

public class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T instance;
    public static bool IsQuitting {get; private set; }

    public static bool HasInstance => instance != null;
    public static T TryGetInstance() => HasInstance ? instance : null;

    public static T Instance
    {
        get
        {
            if (IsQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' requested while quitting. Destroying call.");
                return null;
            }
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    var go = new GameObject(typeof(T).Name + " Auto-Generated");
                    instance = go.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    /// <summary>
    /// Make sure to call base.Awake() in override if you need awake.
    /// </summary>
    protected virtual void Awake()
    {
        IsQuitting = false;
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        if (!Application.isPlaying) return;

        instance = this as T;
    }
    private void OnApplicationQuit()
    {
        IsQuitting = true;
    }
}