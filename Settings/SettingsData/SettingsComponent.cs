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
        public override Type GetSettingType()
        {
            return typeof(HeaderData);
        }
    }

    [Serializable]
    public class HeaderData : SettingData
    {
        public override void UpdateValue<T>(T value)
        {
            //Nothing; 
        }
    }

    [Serializable]
    public abstract class ValueSettingData<T> : SettingsComponentData<ValueData<T>>
    {
        protected ValueSettingData() : base()
        {
        }

        public override Type GetSettingType()
        {
            return typeof(ValueData<T>);
        }
    }

    public class IntSettingData : ValueSettingData<int>
    {
        public Vector2Int bounds = Vector2Int.up;
    }

    public class FloatSettingData : ValueSettingData<float>
    {
        public Vector2 bounds = Vector2.up;
    }

    public class StringSettingData : ValueSettingData<string>
    {

    }

    public class BoolSettingData : ValueSettingData<bool>
    {

    }



}