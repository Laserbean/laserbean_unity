using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Laserbean.General.NewSettings.UI_Viewer
{
    public interface ISettingsGuiItem
    {
        void SetSettingsData(SettingData settingData);
        void SetSettingsComponentData(SettingsComponentData data);

        public event Action<string, string> StringChangeCallback;
        public event Action<string, int> IntChangeCallback;
        public event Action<string, float> FloatChangeCallback;
        public event Action<string, bool> BoolChangeCallback;

        public void UpdateValue(SettingData settingData);

    }
    


}