using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Laserbean.General.OldSettings;
using Laserbean.General.NewSettings.UI_Viewer;

namespace Laserbean.General.NewSettings.UI_Viewer
{
    public class FloatSettingsItem : BasicSettingsItem
    {
        [SerializeField] TMPro.TMP_InputField textinput;

        Vector2 bounds = Vector2.up;

        public override void SetSettingsComponentData(SettingsComponentData data)
        {
            base.SetSettingsComponentData(data);

            textinput.contentType = TMPro.TMP_InputField.ContentType.DecimalNumber;
            textinput.interactable = false;

            if (data is FloatSettingData floatSettingData) {
                bounds = floatSettingData.bounds;
            }
        }

        public override void OnValueChangeFloat(string val)
        {
            if (string.IsNullOrEmpty(val)) return;
            float value = float.Parse(val);
            value = Mathf.Clamp(value, bounds.x, bounds.y);
            textinput.text = "" + value; 

            OnValueChange(value);
        }


        public override void UpdateValue(SettingData value)
        {
            if (value is ValueData<float> bval)
                textinput.text = "" + bval.Value;
        }

    }
}