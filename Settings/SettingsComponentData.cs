using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

using System;

namespace Laserbean.General.NewSettings
{
    [Serializable]
    public abstract class SettingsComponentData : ComponentDataBase
    {

        public string Name;
        public string DisplayName;
        public string Description;
        public abstract Type GetSettingType();

        public GameObject UIPrefab; 

        public abstract SettingData GetDefaultSettingData();
    }


    [Serializable]
    public abstract class SettingsComponentData<TSettingData> : SettingsComponentData where TSettingData : SettingData, new()
    {
        public TSettingData def_value;

        protected SettingsComponentData()
        {
            this.def_value = new();
        }

        public override Type GetSettingType()
        {
            return typeof(TSettingData);
        }

        public override SettingData GetDefaultSettingData() {
            return def_value; 
        }
    }

}