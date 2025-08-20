

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEditor;
using Laserbean.General;


namespace Laserbean.General.ProfilerTools
{
    public static class ProfilerTimer
    {
        public static Dictionary<string, float> TimerDict = new();



        public static void NewTimer(string name)
        {
            if (TimerDict.ContainsKey(name)) {
                Debug.LogWarning("ProfilerTimer contains timer named: " + name);
            }
            TimerDict[name] = Time.realtimeSinceStartup;
        }

        public static void Reset(string name)
        {
            if (!TimerDict.ContainsKey(name)) {
                Debug.LogWarning("ProfilerTimer doesn't contain timer named: " + name);
            }
            TimerDict[name] = Time.realtimeSinceStartup;
        }

        public static void NewOrReset(string name)
        {
            TimerDict[name] = Time.realtimeSinceStartup;
        }

        public static void LogSeconds(string name)
        {
            if (!TimerDict.ContainsKey(name)) {
                Debug.LogWarning("ProfilerTimer can't Log doesn't contain timer named: " + name);
                return;
            }
            Debug.Log(name + ": " + (Time.realtimeSinceStartup - TimerDict[name]).ToString("F7"));
        }

        public static void LogMilliSeconds(string name)
        {
            if (!TimerDict.ContainsKey(name)) {
                Debug.LogWarning("ProfilerTimer can't Log doesn't contain timer named: " + name);
                return;
            }
            Debug.Log(name + ": " + ((Time.realtimeSinceStartup - TimerDict[name])*1000).ToString("F3"));
        }


        public static void ClearAll()
        {
            TimerDict.Clear();
        }

    }
}
