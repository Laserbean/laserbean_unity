using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Laserbean.Input;
using System.Collections.Generic;
using System.Linq;
public class MouseInputables : MonoBehaviour, IMouseInputable2
{

    List<IMouseInputable2> mouseinputables;

    void Start()
    {
        mouseinputables = GetComponentsInChildren<IMouseInputable2>().Where(c => (object) c != this).ToList();
    }
    public bool OnLeftClickDown(Vector2 ScreenPoint)
    {
        bool success = true;
        foreach (var m in mouseinputables)
        {
            success = m.OnLeftClickDown(ScreenPoint) && success;
        }
        return success;
    }

    public void OnLeftClickUp(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnLeftClickUp(ScreenPoint);
        }
    }

    public bool OnLeftDoubleClick(Vector2 ScreenPoint)
    {
        bool success = true;
        foreach (var m in mouseinputables)
        {
            success = m.OnLeftDoubleClick(ScreenPoint) && success;
        }
        return success;
    }

    public void OnLeftDrag(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnLeftDrag(ScreenPoint);
        }
    }

    public void OnLeftDragEnd(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnLeftDragEnd(ScreenPoint);
        }
    }

    public bool OnLeftDragStart(Vector2 ScreenPoint)
    {
        bool success = true;
        foreach (var m in mouseinputables)
        {
            success = m.OnLeftDragStart(ScreenPoint) && success;
        }
        return success;
    }

    public bool OnMiddleClickDown(Vector2 ScreenPoint)
    {
        bool success = true;
        foreach (var m in mouseinputables)
        {
            success = m.OnMiddleClickDown(ScreenPoint) && success;
        }
        return success;
    }

    public void OnMiddleClickUp(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnMiddleClickUp(ScreenPoint);
        }
    }

    public bool OnMiddleDoubleClick(Vector2 ScreenPoint)
    {
        bool success = true;
        foreach (var m in mouseinputables)
        {
            success = m.OnMiddleDoubleClick(ScreenPoint) && success;
        }
        return success;
    }

    public void OnMiddleDrag(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnMiddleDrag(ScreenPoint);
        }
    }

    public void OnMiddleDragEnd(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnMiddleDragEnd(ScreenPoint);
        }
    }

    public bool OnMiddleDragStart(Vector2 ScreenPoint)
    {
        bool success = true;
        foreach (var m in mouseinputables)
        {
            success = m.OnMiddleDragStart(ScreenPoint) && success;
        }
        return success;
    }



    public void OnPointMove(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnPointMove(ScreenPoint);
        }
    }

    public bool OnRightClickDown(Vector2 ScreenPoint)
    {
        bool success = true;
        foreach (var m in mouseinputables)
        {
            success = m.OnRightClickDown(ScreenPoint) && success;
        }
        return success;
    }

    public void OnRightClickUp(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnRightClickUp(ScreenPoint);
        }
    }

    public bool OnRightDoubleClick(Vector2 ScreenPoint)
    {
        bool success = true;
        foreach (var m in mouseinputables)
        {
            success = m.OnRightDoubleClick(ScreenPoint) && success;
        }
        return success;
    }

    public void OnRightDrag(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnRightDrag(ScreenPoint);
        }
    }

    public void OnRightDragEnd(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnRightDragEnd(ScreenPoint);
        }
    }

    public bool OnRightDragStart(Vector2 ScreenPoint)
    {
        bool success = true;
        foreach (var m in mouseinputables)
        {
            success = m.OnRightDragStart(ScreenPoint) && success;
        }
        return success;
    }

    public void OnScroll(Vector2 ScrollDelta, Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnScroll(ScrollDelta, ScreenPoint);
        }
    }
}