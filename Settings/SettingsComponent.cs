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

    [Serializable]
    public class ValueData<T> : SettingData
    {
        public T Value;

    }



    public class IntSetting : ValueSetting<int> { }
    public class FloatSetting : ValueSetting<float> { }
    public class StringSetting : ValueSetting<string> { }
}