
using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

using System;
using Laserbean.SpecialData;



namespace Laserbean.General.NewSettings
{

    [Serializable]
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
    public abstract class ValueData<T> : SettingData
    {
        public T Value { get => value; }
        [SerializeField] protected T value;

        public ValueData() : base()
        {
            value = default;
        }

        public ValueData(ValueData<T> data) : base(data)
        {
            value = data.value;
            valuestring = "" + data.value;
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

    [Serializable]
    public class FloatValueData : ValueData<float>
    {
        public FloatValueData() : base(){ }

        public FloatValueData(FloatValueData data) : base(data)
        {
            value = data.value;
            valuestring = "" + data.value;
        }

        public override SettingData Copy()
        {
            return new FloatValueData(this);
        }

    }
    public class IntValueData : ValueData<int>
    {
        public IntValueData() : base(){ }

        public IntValueData(IntValueData data) : base(data)
        {
            value = data.value;
            valuestring = "" + data.value;
        }

        public override SettingData Copy()
        {
            return new IntValueData(this);
        }
    }

    public class BoolValueData : ValueData<bool>
    {
        public BoolValueData() : base(){ }

        public BoolValueData(BoolValueData data) : base(data)
        {
            value = data.value;
            valuestring = "" + data.value;
        }

        public override SettingData Copy()
        {
            return new BoolValueData(this);
        }
    }
    public class StringValueData : ValueData<string>
    {
        public StringValueData() : base(){ }


        public StringValueData(StringValueData data) : base(data)
        {
            value = data.value;
            valuestring = "" + data.value;
        }

        public override SettingData Copy()
        {
            return new StringValueData(this);
        }
    }


}