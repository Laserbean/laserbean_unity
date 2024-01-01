using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Laserbean.General.EditorAttributes;



namespace Laserbean.General.NewSettings.User
{
    public class SettingsUser : MonoBehaviour
    {
        public List<SettingEvent> settingEvents;




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
