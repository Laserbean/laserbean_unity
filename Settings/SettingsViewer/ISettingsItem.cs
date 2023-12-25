using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.NewSettings.UI_Viewer
{
    public interface ISettingsItem
    {
        void SetSettingsData(SettingData settingData);
        void SetSettingsComponentData(SettingsComponentData data);
    }
}