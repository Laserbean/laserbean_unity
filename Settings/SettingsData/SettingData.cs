
using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

using System;
using Laserbean.SpecialData;



namespace Laserbean.General.NewSettings
{

    public class SettingData
    {
        public string Name;

        [SerializeField, ShowOnly] protected string valuestring;

        protected SettingData()
        {
            Name = "";
        }

        protected SettingData(SettingData data)
        {
            Name = data.Name;
        }

        public virtual SettingData Copy()
        {
            return new SettingData(this);
        }

        public virtual void UpdateValue<T>(T value) { }
    }


    [Serializable]
    public class ValueData<T> : SettingData
    {
        public T Value { get => value; }
        [SerializeField] T value;

        public ValueData() : base()
        {
            value = default;
        }

        public ValueData(ValueData<T> data) : base(data)
        {
            value = data.value;
            valuestring = "" + data.value;
        }

        public override SettingData Copy()
        {
            return new ValueData<T>(this);
        }

        public override void UpdateValue<T1>(T1 val)
        {
            if (val is T castVal) {
                value = castVal;
                valuestring = "" + value;
            }
        }
    }

    [Serializable]
    public struct ValueDataTest<T>
    {
        ValueData<T> Data;
    }

}