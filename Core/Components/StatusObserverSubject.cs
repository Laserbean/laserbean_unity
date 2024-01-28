using System;
using System.Collections;
using System.Collections.Generic;
using Laserbean.General.EditorAttributes;
using Laserbean.General.Observers;
using UnityEngine;
using UnityEngine.Events;

namespace Laserbean.CoreSystem.BasicComponents
{
    [RequireComponent(typeof(StatusFloatCC))]
    public class StatusObserverSubject : ObserverSubject<StatusFloatValueObEvent>
    {

        StatusFloatCC statusFloatCC;
        [SerializeField] string StatusToObserve = "";


        [SerializeField,Foldable] UnityEvent<StatusFloatValue> StatusChanged;
        [SerializeField,Foldable] UnityEvent<StatusFloatValue> StatusMin;
        [SerializeField,Foldable] UnityEvent<StatusFloatValue> StatusMax;

        void Awake()
        {
            statusFloatCC = GetComponent<StatusFloatCC>();

            foreach (var kvp in statusFloatCC.Statuses) {
                if (kvp.Key != StatusToObserve) continue;
                kvp.Value.OnChange += OnStatusChange;
                kvp.Value.OnChange += delegate { StatusChanged.Invoke(new(kvp.Value)); };
                kvp.Value.OnMax += delegate { StatusMax.Invoke(new(kvp.Value)); };
                kvp.Value.OnMin += delegate { StatusMin.Invoke(new(kvp.Value)); };
            }
        }


        private void OnStatusChange(StatusFloat _status)
        {
            NotifyObservers(new StatusFloatValueObEvent(_status));
        }
    }


}