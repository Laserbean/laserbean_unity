using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

using System;

namespace Laserbean.General.NewSettings
{

    [Serializable]
    public class Header : SettingsComponentData<HeaderData>
    {
        public int Size = 1;
    }

    [Serializable]
    public class HeaderData : SettingData
    {
        public override void UpdateValue<T>(T value)
        {
            //Nothing; 
        }
    }


    public class IntSettingData : SettingsComponentData<IntValueData>
    {
        public Vector2Int bounds = Vector2Int.up;
        public bool HideSliderValue = false; 
    }

    public class FloatSettingData : SettingsComponentData<FloatValueData>
    {
        public Vector2 bounds = Vector2.up;
        public bool HideSliderValue = false; 
    }

    public class StringSettingData : SettingsComponentData<StringValueData>
    {

    }

    public class BoolSettingData : SettingsComponentData<BoolValueData>
    {

    }


}