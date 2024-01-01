using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Laserbean.General.EditorAttributes;
using Laserbean.General.NewSettings.Model;
using System;


namespace Laserbean.General.NewSettings.User
{
    public class SettingsUser : MonoBehaviour
    {
        public List<SettingEvent> settingEvents;

        void Awake()
        {
            Settings.OnSettingChange += OnSettingsChange;
        }

        void OnDestroy()
        {
            Settings.OnSettingChange -= OnSettingsChange;
        }

        private void OnSettingsChange(string obj)
        {
            foreach (var settingevent in settingEvents) {
                if (obj == settingevent.Name) {
                    settingevent.RaiseEvent();
                }
            }
        }
    }


    [System.Serializable]
    public class SettingEvent
    {
        public string Name;

        [Foldable(FoldableAttribute.Colour.Blue)] public UnityEvent<int> OnInt;
        [Foldable(FoldableAttribute.Colour.Blue)] public UnityEvent<float> OnFloat;
        [Foldable(FoldableAttribute.Colour.Blue)] public UnityEvent<string> OnString;
        [Foldable(FoldableAttribute.Colour.Blue)] public UnityEvent<bool> OnBool;

        public void RaiseEvent()
        {
            var valuedata = Settings.settings.GetValueData(Name);
            if (valuedata is IntValueData valueData1) OnInt?.Invoke(valueData1.Value); 
            if (valuedata is StringValueData valueData2) OnString?.Invoke(valueData2.Value); 
            if (valuedata is FloatValueData valueData3) OnFloat?.Invoke(valueData3.Value); 
            if (valuedata is BoolValueData valueData4) OnBool?.Invoke(valueData4.Value); 
        }

    }
}
