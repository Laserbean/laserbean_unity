using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Laserbean.SpecialData;
using Laserbean.CoreSystem;
using Laserbean.General.Observers;

namespace Laserbean.CoreSystem.BasicComponents
{

    public class StatusFloatCC : CoreComponent
    {
        public CustomDictionary<string, StatusFloat> Statuses = new();

        [EasyButtons.Button]
        void TestAdd(string nme)
        {
            Statuses.Add(nme, new StatusFloat());
        }
    }

    [Serializable]
    public class StatusFloat
    {
        public float Value = 0;
        public Vector2Int Bounds = Vector2Int.zero;

        public event Action<StatusFloat> OnMin;
        public event Action<StatusFloat> OnMax;
        public event Action<StatusFloat> OnDecrease;
        public event Action<StatusFloat> OnIncrease;
        public event Action<StatusFloat> OnChange;

        bool IsAtBounds = false;

        public StatusFloat()
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

        public static StatusFloat operator +(StatusFloat a, float b)
        {
            if (a.Value >= a.Bounds.y) return a;
            a.Value += b;
            a.OnIncrease?.Invoke(a);
            a.OnChange?.Invoke(a);
            a.DoChecks();
            return a;
        }

        public static StatusFloat operator -(StatusFloat a, float b)
        {
            if (a.Value <= a.Bounds.x) return a;
            a.Value -= b;
            a.OnDecrease?.Invoke(a);
            a.OnChange?.Invoke(a);
            a.DoChecks();
            return a;
        }

    }

    public struct StatusFloatValue : IObserverEvent
    {
        public float Value;
        public Vector2Int Bounds;

        public StatusFloatValue(StatusFloat _status)
        {
            Value = _status.Value;
            Bounds = _status.Bounds;
        }

        public float Percentage { get => Value / (Bounds.y - Bounds.x); }
    }

}
