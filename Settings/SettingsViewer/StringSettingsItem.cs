using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Laserbean.General.OldSettings;
using Laserbean.General.NewSettings.UI_Viewer;

namespace Laserbean.General.NewSettings
{
    public class StringSettingsItem : BasicSettingsItem
    {
        [SerializeField] TMPro.TMP_InputField textinput;

        public override void SetSettingsComponentData(SettingsComponentData data)
        {
            base.SetSettingsComponentData(data);

            textinput.interactable = false;
        }
    }
}