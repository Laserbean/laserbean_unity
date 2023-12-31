
using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

using System;
using Laserbean.SpecialData;



namespace Laserbean.General.NewSettings.Model
{
    [Serializable]
    public class SettingsData
    {
        public CustomDictionary<string, SettingData> Data { get => _Data; }

        CustomDictionary<string, SettingData> _Data = new();

        public SettingData GetValueData(string name)
        {
            if (!Data.ContainsKey(name)) {
                throw new KeyNotFoundException("Key of " + name + " not found in Settings Data");
            }
            return Data[name];
        }

        public void UpdateValueData<T>(string name, T value)
        {
            if (!Data.ContainsKey(name)) {
                throw new KeyNotFoundException("Key of " + name + " not found in Settings Data");
            }
            Data[name].UpdateValue(value);
        }

        public void AddDefaultSettingsData(SettingData settingdata)
        {
            if (_Data.ContainsKey(settingdata.Name)) {
                Debug.Log("HHMMMM??? compdata contains key");
                return;
            }
            _Data.Add(settingdata.Name, settingdata);
        }
    }
}