using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Laserbean.General.NewSettings.UI_Viewer;
using Laserbean.General.NewSettings;

public class HeaderSettingsItem : MonoBehaviour, ISettingsItem
{
    [SerializeField] TMPro.TextMeshProUGUI label;


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
}
