using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Laserbean.General.OldSettings;

namespace Laserbean.General.NewSettings.UI_Viewer
{
    public class BasicSettingsItem : MonoBehaviour, ISettingsItem
    {
        [SerializeField] TMPro.TextMeshProUGUI label;
        [SerializeField] TMPro.TextMeshProUGUI description;


        public virtual void SetSettingsData(SettingData settingData)
        {
            throw new System.NotImplementedException();
        }

        public virtual void SetSettingsComponentData(SettingsComponentData data)
        {
            label.text = data.DisplayName; 
            description.text = data.Description; 
            
        }
    }
}