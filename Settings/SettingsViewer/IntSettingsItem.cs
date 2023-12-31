using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Laserbean.General.OldSettings;
using Laserbean.General.NewSettings.UI_Viewer;

namespace Laserbean.General.NewSettings.UI_Viewer
{
    public class IntSettingsItem : BasicSettingsItem
    {
        [SerializeField] TMPro.TMP_InputField textinput;

        public override void SetSettingsComponentData(SettingsComponentData data)
        {
            base.SetSettingsComponentData(data);

            textinput.contentType = TMPro.TMP_InputField.ContentType.IntegerNumber;
            // textinput.text = "" + (SettingsManager.Instance.GetInt(_name));
            textinput.interactable = false;

        }


        public override void UpdateValue(SettingData value)
        {
            if (value is ValueData<int> bval)
                textinput.text = "" + bval.Value;
        }

    }
}