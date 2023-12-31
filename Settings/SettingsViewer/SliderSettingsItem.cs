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

        [SerializeField] TMPro.TextMeshProUGUI value_text;


        public override void SetSettingsComponentData(SettingsComponentData data)
        {
            base.SetSettingsComponentData(data);

            if (data is FloatSettingData fdata) {
                slider.minValue = fdata.bounds.x;
                slider.maxValue = fdata.bounds.y;

                if (fdata.HideSliderValue) value_text.gameObject.SetActive(false); 
            }

            if (data is IntSettingData idata) {
                slider.minValue = idata.bounds.x;
                slider.maxValue = idata.bounds.y;
                slider.wholeNumbers = true;
                
                if (idata.HideSliderValue) value_text.gameObject.SetActive(false); 
            }
        }

        float currentvalue = 0f;

        public override void OnValueChangeFloat(System.Single val)
        {
            currentvalue = val;
            currentvalue = Mathf.Round(currentvalue * 100f) / 100f;
            value_text.text = "" + currentvalue.ToString("0.##");
        }


        public void OnSliderRelease()
        {
            if (slider.wholeNumbers) {
                OnValueChange(Mathf.RoundToInt(currentvalue));
            } else {
                OnValueChange(currentvalue);
            }
            slider.value = currentvalue;
        }




        public override void UpdateValue(SettingData value)
        {
            if (value is ValueData<float> bval) {
                slider.value = bval.Value;
                value_text.text = "" + slider.value;
            }
        }

    }
}