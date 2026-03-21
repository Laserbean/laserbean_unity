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
        mouseinputables = GetComponentsInChildren<IMouseInputable2>().Where(c => (object)c != this).ToList();
    }
    public void OnLeftClickDown(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnLeftClickDown(ScreenPoint);
        }
        ;
    }

    public void OnLeftClickUp(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnLeftClickUp(ScreenPoint);
        }
    }

    public void OnLeftDoubleClick(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnLeftDoubleClick(ScreenPoint);
        }
        ;
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

    public void OnLeftDragStart(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnLeftDragStart(ScreenPoint);
        }
        ;
    }

    public void OnMiddleClickDown(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnMiddleClickDown(ScreenPoint);
        }
        ;
    }

    public void OnMiddleClickUp(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnMiddleClickUp(ScreenPoint);
        }
    }

    public void OnMiddleDoubleClick(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnMiddleDoubleClick(ScreenPoint);
        }
        ;
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

    public void OnMiddleDragStart(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnMiddleDragStart(ScreenPoint);
        }
        ;
    }



    public void OnPointMove(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnPointMove(ScreenPoint);
        }
    }

    public void OnRightClickDown(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnRightClickDown(ScreenPoint);
        }
        ;
    }

    public void OnRightClickUp(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnRightClickUp(ScreenPoint);
        }
    }

    public void OnRightDoubleClick(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnRightDoubleClick(ScreenPoint);
        }
        ;
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

    public void OnRightDragStart(Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnRightDragStart(ScreenPoint);
        }
        ;
    }

    public void OnScroll(Vector2 ScrollDelta, Vector2 ScreenPoint)
    {
        foreach (var m in mouseinputables)
        {
            m.OnScroll(ScrollDelta, ScreenPoint);
        }
    }
}