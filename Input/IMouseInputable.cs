using UnityEngine;

public interface IMouseInputable
{
    public void OnClickDown(Vector2 ScreenPoint);
    public void OnClickUp(Vector2 ScreenPoint);
    public void OnDrag(Vector2 ScreenPoint);
    public void OnPointMove(Vector2 ScreenPoint);
}
