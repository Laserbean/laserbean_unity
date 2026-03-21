using UnityEngine;

namespace Laserbean.Input
{

    public interface IMouseInputable
    {
        // Original methods (keep for backward compatibility)
        public void OnClickDown(Vector2 ScreenPoint);
        public void OnClickUp(Vector2 ScreenPoint);
        public void OnDrag(Vector2 ScreenPoint);
        public void OnPointMove(Vector2 ScreenPoint);

    }

    public interface IMouseInputable2
    {
        // New left click methods

        public void OnPointMove(Vector2 ScreenPoint);

        public void OnLeftClickDown(Vector2 ScreenPoint);
        public void OnLeftClickUp(Vector2 ScreenPoint);
        public void OnLeftDragStart(Vector2 ScreenPoint);
        public void OnLeftDrag(Vector2 ScreenPoint);
        public void OnLeftDragEnd(Vector2 ScreenPoint);
        public void OnLeftDoubleClick(Vector2 ScreenPoint);

        // Right click methods
        public void OnRightClickDown(Vector2 ScreenPoint);
        public void OnRightClickUp(Vector2 ScreenPoint);
        public void OnRightDragStart(Vector2 ScreenPoint);
        public void OnRightDrag(Vector2 ScreenPoint);
        public void OnRightDragEnd(Vector2 ScreenPoint);
        public void OnRightDoubleClick(Vector2 ScreenPoint);


        // Middle click methods
        public void OnMiddleClickDown(Vector2 ScreenPoint);
        public void OnMiddleClickUp(Vector2 ScreenPoint);
        public void OnMiddleDragStart(Vector2 ScreenPoint);
        public void OnMiddleDrag(Vector2 ScreenPoint);
        public void OnMiddleDragEnd(Vector2 ScreenPoint);

        public void OnMiddleDoubleClick(Vector2 ScreenPoint);


        // Scroll wheel
        public void OnScroll(Vector2 ScrollDelta, Vector2 ScreenPoint);
    }
}
