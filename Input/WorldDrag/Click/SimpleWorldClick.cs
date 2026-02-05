using System;
using Laserbean.General;
using UnityEngine;
using UnityEngine.Events;

public class SimpleWorldClick : MonoBehaviour, IWorldClickable
{
    [SerializeField]
    UnityEvent OnClickDownEvent;
    [SerializeField]
    UnityEvent OnClickReleasedEvent;
    [SerializeField]
    UnityEvent OnClickEvent;

    [SerializeField] float click_time = 0.2f;

    StopwatchTimer clicktimer;
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

    public void OnClickPressed()
    {
        // Debug.Log(name + " clicked!");
        clicktimer.Reset();

        clicktimer.Start();
        OnClickDownEvent.Invoke();
    }

    public void OnClickReleased()
    {
        clicktimer.Stop();
        OnClickReleasedEvent.Invoke();


        // Debug.Log(name + " released!");
    }

}
