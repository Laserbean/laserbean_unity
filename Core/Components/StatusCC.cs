using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Laserbean.SpecialData;
using Laserbean.CoreSystem;

namespace Laserbean.CoreSystem.BasicComponents
{

    public class StatusCC : CoreComponent
    {
        public CustomDictionary<string, Status> Statuses = new();

        [EasyButtons.Button]
        void TestAdd(string nme)
        {
            Statuses.Add(nme, new Status());
        }
    }


    [Serializable]
    public class Status
    {
        public int Value = 0;
        public Vector2Int Bounds = Vector2Int.zero;

        public event Action<Status> OnMin;
        public event Action<Status> OnMax;
        public event Action<Status> OnDecrease;
        public event Action<Status> OnIncrease;
        public event Action<Status> OnChange;

        bool IsAtBounds = false;

        public Status()
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

        public static Status operator +(Status a, int b)
        {
            if (a.Value >= a.Bounds.y) return a;
            a.Value += b;
            a.OnIncrease?.Invoke(a);
            a.OnChange?.Invoke(a);
            a.DoChecks();
            return a;
        }

        public static Status operator -(Status a, int b)
        {
            if (a.Value <= a.Bounds.x) return a;
            a.Value -= b;
            a.OnDecrease?.Invoke(a);
            a.OnChange?.Invoke(a);
            a.DoChecks();
            return a;
        }

    }

    public struct StatusValue
    {
        public int Value;
        public Vector2Int Bounds;

        public StatusValue(Status _status)
        {
            Value = _status.Value;
            Bounds = _status.Bounds;
        }

        public float Percentage { get => Value / (Bounds.y - Bounds.x); }
    }

}
