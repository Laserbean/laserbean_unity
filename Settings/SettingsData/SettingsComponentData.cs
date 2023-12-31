using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

using System;
using UnityEngine.Search;
using Laserbean.General.NewSettings.UI_Viewer;

namespace Laserbean.General.NewSettings
{
    [Serializable]
    public abstract class SettingsComponentData : ComponentDataBase
    {

        [RedIfEmpty]
        public string Name;
        
        public string DisplayName;
        public string Description;

        [SearchContext("t:prefab *Setting", "asset")]
        public GameObject UIPrefab;

        public abstract SettingData GetDefaultSettingData();

        public abstract void SetValueName(string name);
        public abstract string GetValueName();


    }


    [Serializable]
    public abstract class SettingsComponentData<TSettingData> : SettingsComponentData where TSettingData : SettingData, new()
    {
        public TSettingData def_value;

        protected SettingsComponentData()
        {
            this.def_value = new();
        }

        public override string GetValueName()
        {
            return def_value.Name;
        }

        public override void SetValueName(string name)
        {
            def_value.Name = name;
        }


        public override SettingData GetDefaultSettingData()
        {
            return def_value.Copy();
        }
    }

}