using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General.NewSettings;
using System;
using Laserbean.General.NewSettings.Presenter;


namespace Laserbean.General.NewSettings.UI_Viewer
{
    public class SettingsViewer : MonoBehaviour
    {

        [SerializeField] Transform content_transform;

        Dictionary<string, ISettingsGuiItem> guiItemDict = new();



        internal void UpdateSettingData(string name, SettingData value)
        {
            guiItemDict[name].UpdateValue(value);
        }

        internal void AddSettingComponentData(SettingsComponentData settingsComponentData)
        {
            if (content_transform == null) return;

            GameObject go = Instantiate(settingsComponentData.UIPrefab);
            go?.transform.SetParent(content_transform);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;

            ISettingsGuiItem settingsGuiItem = go.GetComponentInChildren<ISettingsGuiItem>();
            settingsGuiItem?.SetSettingsComponentData(settingsComponentData);
            settingsGuiItem.FloatChangeCallback += FloatChangeCallback;
            settingsGuiItem.IntChangeCallback += IntChangeCallback;
            settingsGuiItem.BoolChangeCallback += BoolChangeCallback;
            settingsGuiItem.StringChangeCallback += StringChangeCallback;

            guiItemDict.Add(settingsComponentData.Name, settingsGuiItem);
        }

        private void StringChangeCallback(string arg1, string arg2)
        {
            SettingsPresenter.Instance.StringChangeCallback(arg1, arg2);
        }

        private void BoolChangeCallback(string arg1, bool arg2)
        {
            SettingsPresenter.Instance.BoolChangeCallback(arg1, arg2);
        }

        private void IntChangeCallback(string arg1, int arg2)
        {
            SettingsPresenter.Instance.IntChangeCallback(arg1, arg2);
        }

        private void FloatChangeCallback(string arg1, float arg2)
        {
            SettingsPresenter.Instance.FloatChangeCallback(arg1, arg2);
        }


    }
}