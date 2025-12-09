using UnityEngine;
using Laserbean.CustomUnityEvents;

[CreateAssetMenu(fileName = "PopupInfoGameEvent", menuName = "Scriptable Objects/Laserbean Game Events/PopupInfoGameEvent", order = 99)]
public class TextPopupGameEvent : GenericGameEvent<PopupInfo>
{
    [Header("PopupInfo Game Event"), SerializeField]
    [ShowOnly] string description = "This is a PopupInfo Game Event";
}

[System.Serializable]

public struct PopupInfo
{
    public string Text;
    public float Lifetime;
    public Vector2 Position;
    public Vector2 Force;

    public PopupInfo(string text, float lifetime, Vector2 position)
    {
        Text = text;
        Position = position;
        Force = Vector2.zero;
        Lifetime = lifetime;
    }

    public PopupInfo(string text, float lifetime, Vector2 position, Vector2 force)
    {
        Text = text;
        Position = position;
        Force = force;
        Lifetime = lifetime;
    }
}