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
    public abstract class ValueSetting<T> : SettingsComponentData<ValueData<T>>
    {
        protected ValueSetting() : base()
        {
        }

        public override Type GetSettingType()
        {
            return typeof(ValueData<T>);
        }
    }

    public class IntSetting : ValueSetting<int>
    {
        public Vector2Int bounds = Vector2Int.up;
    }

    public class FloatSetting : ValueSetting<float>
    {
        public Vector2 bounds = Vector2.up;
    }

    public class StringSetting : ValueSetting<string>
    {

    }

    public class BoolSetting : ValueSetting<bool>
    {

    }



}