using UnityEngine;

public interface IMouseInputs
{
    public void OnPointMove(Vector2 ScreenPoint);

    public void OnClickDown(Vector2 ScreenPoint);
    public void OnClickUp(Vector2 ScreenPoint);
    public void OnDragStart(Vector2 ScreenPoint);
    public void OnDrag(Vector2 ScreenPoint);
    // public void OnHold(Vector2 ScreenPoint);
    public void OnDragEnd(Vector2 ScreenPoint);
    public void OnDoubleClick(Vector2 ScreenPoint);
    public void OnHoldDown(Vector2 ScreenPoint);
    public void OnHoldUp(Vector2 ScreenPoint);
}
