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
        public static Action<string> OnSettingChange { get => Settings.OnSettingChange; set => Settings.OnSettingChange = value; }

        [SerializeField] SettingsObject settingsObject;

        public SettingsViewer Viewer;

        // [SerializeField] SettingsData settings;

        string SettingsPath { get => GameManager.Instance.AppPath; }
        const string SettingsFileName = "Settings";

        SettingsData GlobalSettings { get => Settings.settings; set => Settings.settings = value; }


        void Awake()
        {
            GlobalSettings = new();
        }

        void Start()
        {
            InitializeSettings();
            InitializeSettingsViewer();

            UpdateSettingsViewer();
        }

        void UpdateSettingsViewer()
        {
            foreach (var kvp in GlobalSettings.Data) {
                Viewer.UpdateSettingData(kvp.Key, kvp.Value);
            }
        }

        public void UpdateSetting<T>(string name, ValueData<T> value)
        {
            GlobalSettings.UpdateValueData(name, value.Value);
            Viewer.UpdateSettingData(name, value);
        }


        void InitializeSettings()
        {
            foreach (SettingsComponentData fish in settingsObject.ComponentData.Cast<SettingsComponentData>()) {
                GlobalSettings.AddDefaultSettingsData(fish.GetDefaultSettingData());
            }

        }

        private void InitializeSettingsViewer()
        {
            foreach (SettingsComponentData settingsComponentData in settingsObject.ComponentData.Cast<SettingsComponentData>()) {
                Viewer.AddSettingComponentData(settingsComponentData);
            }
        }

        [EasyButtons.Button]
        public void SaveSettings()
        {
            SaveAnything.SaveJsonPretty(GlobalSettings, SettingsPath, SettingsFileName);
        }

        [EasyButtons.Button]
        public void LoadSettings()
        {
            GlobalSettings = SaveAnything.LoadJson<SettingsData>(SettingsPath, SettingsFileName);
            UpdateSettingsViewer();
        }


        public T GetValue<T>(string name)
        {
            var fish = GlobalSettings.GetValueData(name);
            if (fish is ValueData<T> vdata)
                return vdata.Value;
            throw new Exception("" + name + " is not a " + typeof(T) + " ValueData");
        }

        public void StringChangeCallback(string arg1, string arg2)
        {
            GlobalSettings.UpdateValueData(arg1, arg2);
        }

        public void BoolChangeCallback(string arg1, bool arg2)
        {
            GlobalSettings.UpdateValueData(arg1, arg2);
        }

        public void IntChangeCallback(string arg1, int arg2)
        {
            GlobalSettings.UpdateValueData(arg1, arg2);
        }

        public void FloatChangeCallback(string arg1, float arg2)
        {
            GlobalSettings.UpdateValueData(arg1, arg2);
        }


    }
}