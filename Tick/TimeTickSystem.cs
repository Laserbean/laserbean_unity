using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace Laserbean.General.GlobalTicks
{


public class TimeTickSystem : MonoBehaviour {

    public class OnTickEventArgs : EventArgs {
        public int tick; 
    }

    public static event EventHandler<OnTickEventArgs> OnTick;

    public const float TICK_TIME = .2f; 


    private int tick = 0;
    private float tickTimer;


    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIME) {
            tickTimer -= TICK_TIME; 
            tick++;
                OnTick?.Invoke(this, new OnTickEventArgs { tick = tick });
                // CMDebug.TextPopupMouseFontsize("tick".DebugColor(Color.green) + tick); 
        }


        
    }

    private void OnDisable() {
        OnTick = null; 
    }



}

public class TickTimer {
    int start_tick = 0; 
    int timer_tick; 

    int current_tick=0; 

    public TickTimer(int period) {
        timer_tick = period; 
        TimeTickSystem.OnTick += OnTick; 

    }

    public TickTimer(float time) {
        SetTimerSeconds(time); 
        TimeTickSystem.OnTick += OnTick; 
    }

    void OnTick(object sender, TimeTickSystem.OnTickEventArgs eventArgs) {
        current_tick = eventArgs.tick; 
    }

    public void StartTimer() {
        start_tick = current_tick; 
    }

    public bool HasFinished() {
        return (current_tick > start_tick + timer_tick); 
    }

    public void SetTimer(int new_period) {
        timer_tick = new_period; 
    }

    public void SetTimerSeconds(float time) {
        timer_tick = Mathf.RoundToInt(time / TimeTickSystem.TICK_TIME);
    }
}



//usage:
// TimeTickSystem.OnTick += delegate (object sender, TimeTickSystem.OnTickEventArgs e) {
// //DO stuff
//}

}