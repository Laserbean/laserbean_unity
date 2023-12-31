using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Laserbean.General.OldSettings;
using Laserbean.General.NewSettings.UI_Viewer;

namespace Laserbean.General.NewSettings.UI_Viewers
{
    public class SliderSettingsItem : BasicSettingsItem
    {
        [SerializeField] Slider slider;

        public override void SetSettingsComponentData(SettingsComponentData data)
        {
            base.SetSettingsComponentData(data);
        }


        public override void UpdateValue(SettingData value)
        {
            if (value is ValueData<float> bval)
                slider.value = bval.Value;
        }

    }
}