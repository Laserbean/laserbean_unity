using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeMonkey;
using System;

namespace Laserbean.General.GlobalTicks
{


public class GlobalTickSystem : MonoBehaviour {

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
            if (OnTick != null) OnTick(this, new OnTickEventArgs{tick=tick});
            CMDebug.TextPopupMouseFontsize("tick".DebugColor(Color.green) + tick); 
        }


        
    }

    private void OnDisable() {
        OnTick = null; 
    }


}

//usage:
// GlobalTickSystem.OnTick += delegate (object sender, GlobalTickSystem.OnTickEventArgs e) {
// //DO stuff
//}

}