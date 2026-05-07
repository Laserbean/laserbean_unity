using System;
using Laserbean.General;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SimpleWorldClick : MonoBehaviour, IWorldClickable
{
    [SerializeField] UnityEvent OnClickDownEvent;
    [SerializeField] UnityEvent OnClickReleasedEvent;
    [SerializeField] UnityEvent OnClickInterruptedEvent;
    [SerializeField]
    UnityEvent OnClickEvent;

    [SerializeField] float click_time = 0.2f;

    StopwatchTimer clicktimer;

    [SerializeField] public bool Clickable1 = true;


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
        if (!Clickable1) return;
        // Debug.Log(name + " clicked!");
        clicktimer.Reset();

        clicktimer.Start();
        OnClickDownEvent?.Invoke();

        isInterrupted = false; 

        // CustomCursor.Instance.SetCursorByType(CursorType.Clicking); 
    }

    public void OnClickUp()
    {
        if (!Clickable1) return;

        if (isInterrupted) return;

        clicktimer.Stop();
        OnClickReleasedEvent?.Invoke();

        // CustomCursor.Instance.SetCursorByType(CursorType.Clickable); 

        // Debug.Log(name + " released!");
    }

    public void SetClickable(bool click_able)
    {
        Clickable1 = click_able;
    }

    public void OnClickInterrupt()
    {
        if (!Clickable1) return;
        clicktimer.Stop();

        isInterrupted = true;
        OnClickInterruptedEvent?.Invoke();
    }

    [ShowOnly, SerializeField]
    bool isInterrupted = false;
}
