using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General.NewSettings;
using System;


namespace Laserbean.General.NewSettings.UI_Viewer
{
    public class SettingsViewer : MonoBehaviour
    {
        [SerializeField] SettingsObject settings;

        [SerializeField] Transform content_transform;


        private void Start()
        {
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            if (content_transform == null) return;

            foreach (var fish in settings.ComponentData) {
                SettingsComponentData settingsComponentData = fish as SettingsComponentData;
                GameObject go = Instantiate(settingsComponentData.UIPrefab);
                go?.transform.SetParent(content_transform);
                go.transform.localScale = Vector3.one; 
                go.transform.localPosition = Vector3.zero; 

                go.GetComponentInChildren<ISettingsItem>()?.SetSettingsComponentData(settingsComponentData);
            }
        }
    }
}