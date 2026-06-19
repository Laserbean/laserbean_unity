using System;
using Laserbean.General;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SimpleWorldClick : MonoBehaviour, IWorldClickable, IWorldInteractable
{
    [SerializeField] UnityEvent OnClickDownEvent;
    [SerializeField] UnityEvent OnClickReleasedEvent;
    [SerializeField] UnityEvent OnClickInterruptedEvent;
    [SerializeField]
    UnityEvent OnClickEvent;

    [SerializeField] float click_time = 0.2f;

    StopwatchTimer clicktimer;

    [SerializeField] public bool isClickable = true;


    private void Awake()
    {
        clicktimer = new();
        clicktimer.OnTimerStop += OnTimerEnd;

    }

    void FixedUpdate()
    {
        clicktimer.Tick(Time.deltaTime);
    }


    private void OnTimerEnd()
    {
        if (clicktimer.Time < click_time)
        {
            OnClickEvent.Invoke();
        }
        // OnClickFastEvent.Invoke();
    }

    public void OnClickDown()
    {
        if (!isClickable) return;
        // Debug.Log(name + " clicked!");
        clicktimer.Reset();

        clicktimer.Start();
        OnClickDownEvent?.Invoke();

        isInterrupted = false;

        // CustomCursor.Instance.SetCursorByType(CursorType.Clicking); 
    }

    public void OnClickUp()
    {
        if (!isClickable) return;

        if (isInterrupted) return;

        clicktimer.Stop();
        OnClickReleasedEvent?.Invoke();

        // CustomCursor.Instance.SetCursorByType(CursorType.Clickable); 

        // Debug.Log(name + " released!");
    }

    public void SetClickable(bool click_able)
    {
        isClickable = click_able;
        if (isClickable)
        {
            OnSetClickable.Invoke();
        }
        else
        {
            OnSetUnclickable.Invoke();
        }
    }
    [SerializeField] UnityEvent OnSetClickable;
    [SerializeField] UnityEvent OnSetUnclickable;

    public void OnClickInterrupt()
    {
        if (!isClickable) return;
        clicktimer.Stop();

        isInterrupted = true;
        OnClickInterruptedEvent?.Invoke();
    }

    public void OnDoubleClick()
    {
        Debug.Log("Double clicked success!");
    }

    public void SetInteractable(bool canInteract)
    {
        SetClickable(canInteract);
    }

    public bool IsInteractable()
    {
        return isClickable;
    }

    [ShowOnly, SerializeField]
    bool isInterrupted = false;
}
