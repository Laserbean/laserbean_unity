using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Laserbean.General.NewSettings.User
{
    public class SettingsUser : MonoBehaviour
    {
        // public List<SettingEvent> settingEvents;

        [Foldable]
        public int fish;

        
        public int fish2; 
        [Foldable]
        public UnityEvent unityEvent;
        
        // public UnityEvent unityEvents2;

        // [Expandable]
        // public SettingsObject settingsObject; 
    }


    [System.Serializable]
    public class SettingEvent
    {
        public string Name;

        [Foldable]
        public UnityEvent unityEvents;

        // public void AddInt() {
        //     unityEvents.Add
        // }

    }
}
