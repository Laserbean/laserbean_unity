using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Laserbean.General.OldSettings;
using System;

namespace Laserbean.General.NewSettings.UI_Viewer
{
    public class BasicSettingsItem : MonoBehaviour, ISettingsGuiItem
    {
        [SerializeField] TMPro.TextMeshProUGUI label;
        [SerializeField] TMPro.TextMeshProUGUI description;

        public event Action<string, string> StringChangeCallback;
        public event Action<string, int> IntChangeCallback;
        public event Action<string, float> FloatChangeCallback;
        public event Action<string, bool> BoolChangeCallback;


        void OnDestroy()
        {
            StringChangeCallback = null;
            IntChangeCallback = null;
            FloatChangeCallback = null;
            BoolChangeCallback = null;
        }


        public virtual void SetSettingsData(SettingData settingData)
        {
            throw new System.NotImplementedException();
        }

        protected string name_id = "";

        public virtual void SetSettingsComponentData(SettingsComponentData data)
        {
            name_id = data.Name;
            label.text = data.DisplayName;
            description.text = data.Description;

        }

        public virtual void OnValueChange(string val)
        {
            Debug.Log("string: " + val);
            if (string.IsNullOrEmpty(name_id)) Debug.LogError("setting value is empty");

            StringChangeCallback?.Invoke(name_id, val);
        }


        public virtual void OnValueChangeFloat(System.Single val)
        {
            OnValueChange(val);
        }


        public virtual void OnValueChangeInt(string val)
        {
            if (string.IsNullOrEmpty(val)) return;
            if (string.IsNullOrEmpty(name_id)) Debug.LogError("setting value is empty");
            OnValueChange(int.Parse(val));
        }

        public virtual void OnValueChangeFloat(string val)
        {
            if (string.IsNullOrEmpty(val)) return;
            OnValueChange(float.Parse(val));
        }

        public virtual void OnValueChange(bool val)
        {
            Debug.Log("bool: " + val);
            if (string.IsNullOrEmpty(name_id)) Debug.LogError("setting value is empty");
            BoolChangeCallback?.Invoke(name_id, val);
        }

        protected virtual void OnValueChange(int val)
        {
            Debug.Log("int: " + val);
            if (string.IsNullOrEmpty(name_id)) Debug.LogError("setting value is empty");
            IntChangeCallback?.Invoke(name_id, val);
        }

        protected virtual void OnValueChange(float val)
        {
            Debug.Log("float: " + val);
            if (string.IsNullOrEmpty(name_id)) Debug.LogError("setting value is empty");
            FloatChangeCallback?.Invoke(name_id, val);
        }

        public virtual void UpdateValue(SettingData value)
        {
            
        }
    }
}