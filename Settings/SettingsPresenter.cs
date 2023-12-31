using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Laserbean.General.NewSettings.Model;

using Laserbean.General.NewSettings.UI_Viewer;
using System.Linq;
using System;


namespace Laserbean.General.NewSettings.Presenter
{
    public class SettingsPresenter : Singleton<SettingsPresenter>
    {
        public static Action<string> OnSettingChange; 

        [SerializeField] SettingsObject settingsObject;

        public SettingsViewer Viewer;

        [SerializeField] Settings settings;


        void Awake()
        {
            settings = new();
        }

        void Start()
        {
            InitializeSettings();
            InitializeSettingsViewer();

            foreach(var kvp in settings.Data) {
                Viewer.UpdateSettingData(kvp.Key, kvp.Value);
            }
        }

        public void UpdateSetting<T>(string name, ValueData<T> value)
        {
            settings.UpdateValueData(name, value.Value);
            Viewer.UpdateSettingData(name, value);
        }


        void InitializeSettings()
        {
            foreach (SettingsComponentData fish in settingsObject.ComponentData.Cast<SettingsComponentData>()) {
                settings.AddDefaultSettingsData(fish.GetDefaultSettingData());
            }

        }

        private void InitializeSettingsViewer()
        {
            foreach (SettingsComponentData settingsComponentData in settingsObject.ComponentData.Cast<SettingsComponentData>()) {
                Viewer.AddSettingComponentData(settingsComponentData);
            }
        }


        public void StringChangeCallback(string arg1, string arg2)
        {
            settings.UpdateValueData(arg1, arg2);
            OnSettingChange?.Invoke(arg1);
        }

        public void BoolChangeCallback(string arg1, bool arg2)
        {
            settings.UpdateValueData(arg1, arg2);
            OnSettingChange?.Invoke(arg1);
        }

        public void IntChangeCallback(string arg1, int arg2)
        {
            settings.UpdateValueData(arg1, arg2);
            OnSettingChange?.Invoke(arg1);
        }

        public void FloatChangeCallback(string arg1, float arg2)
        {
            settings.UpdateValueData(arg1, arg2);
            OnSettingChange?.Invoke(arg1);
        }


    }
}