using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Laserbean.General.NewSettings.UI_Viewer;
using Laserbean.General.NewSettings;
using System;

namespace Laserbean.General.NewSettings.UI_Viewer
{
    public class HeaderSettingsItem : MonoBehaviour, ISettingsGuiItem
    {
        [SerializeField] TMPro.TextMeshProUGUI label;

        public event Action<string, string> StringChangeCallback;
        public event Action<string, int> IntChangeCallback;
        public event Action<string, float> FloatChangeCallback;
        public event Action<string, bool> BoolChangeCallback;

        void SetHeader(string val)
        {
            label.text = val;
        }

        public void SetSettingsData(SettingData settingData)
        {
        }

        public void SetSettingsComponentData(SettingsComponentData data)
        {
            if (!(data is Header)) return;
            SetHeader(data.DisplayName);
        }

        public void UpdateValue(SettingData settingData)
        {
        }
    }
}