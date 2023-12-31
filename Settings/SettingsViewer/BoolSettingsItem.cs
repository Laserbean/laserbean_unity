using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Laserbean.General.OldSettings;
using Laserbean.General.NewSettings.UI_Viewer;

namespace Laserbean.General.NewSettings.UI_Viewer
{
    public class BoolSettingsItem : BasicSettingsItem
    {
        [SerializeField] GameObject toggle;

        public override void SetSettingsComponentData(SettingsComponentData data)
        {
            base.SetSettingsComponentData(data);
            toggle.SetActive(true);
            // toggle.GetComponent<Toggle>().isOn = SettingsManager.Instance.GetBool(_name);
        }

        public override void UpdateValue(SettingData value)
        {
            if (value is ValueData<bool> bval)
                toggle.GetComponentInChildren<Toggle>().isOn = bval.Value;
        }
    }
}