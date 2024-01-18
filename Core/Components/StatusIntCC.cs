using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Laserbean.SpecialData;
using Laserbean.CoreSystem;

namespace Laserbean.CoreSystem.BasicComponents
{

    public class StatusIntCC : CoreComponent
    {
        public CustomDictionary<string, StatusInt> Statuses = new();

        [EasyButtons.Button]
        void TestAdd(string nme)
        {
            Statuses.Add(nme, new StatusInt());
        }
    }


    [Serializable]
    public class StatusInt
    {
        public int Value = 0;
        public Vector2Int Bounds = Vector2Int.zero;

        public event Action<StatusInt> OnMin;
        public event Action<StatusInt> OnMax;
        public event Action<StatusInt> OnDecrease;
        public event Action<StatusInt> OnIncrease;
        public event Action<StatusInt> OnChange;

        bool IsAtBounds = false;

        public StatusInt()
        {
            Value = 0;
            OnMin = null;
            OnMax = null;
            OnDecrease = null;
            OnIncrease = null;
            OnChange = null;
            IsAtBounds = false;
            Bounds = Vector2Int.zero;
        }

        void DoChecks()
        {
            Value = Mathf.Clamp(Value, Bounds.x, Bounds.y);

            if (!IsAtBounds && (Value == Bounds.x || Value == Bounds.y)) {
                IsAtBounds = true;
                if (Value >= Bounds.y) OnMax?.Invoke(this);
                if (Value <= Bounds.x) OnMin?.Invoke(this);
            } else if (Value > Bounds.x && Value < Bounds.y) {
                IsAtBounds = false;
            }
        }

        public static StatusInt operator +(StatusInt a, int b)
        {
            if (a.Value >= a.Bounds.y) return a;
            a.Value += b;
            a.OnIncrease?.Invoke(a);
            a.OnChange?.Invoke(a);
            a.DoChecks();
            return a;
        }

        public static StatusInt operator -(StatusInt a, int b)
        {
            if (a.Value <= a.Bounds.x) return a;
            a.Value -= b;
            a.OnDecrease?.Invoke(a);
            a.OnChange?.Invoke(a);
            a.DoChecks();
            return a;
        }

    }


    public struct StatusIntValue
    {
        public int Value;
        public Vector2Int Bounds;

        public StatusIntValue(StatusInt _status)
        {
            Value = _status.Value;
            Bounds = _status.Bounds;
        }

        public float Percentage { get => Value / (Bounds.y - Bounds.x); }
    }

}
