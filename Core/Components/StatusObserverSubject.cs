using System;
using System.Collections;
using System.Collections.Generic;
using Laserbean.General.Observers;
using UnityEngine;

namespace Laserbean.CoreSystem.BasicComponents
{
    [RequireComponent(typeof(StatusFloatCC))]
    public class StatusObserverSubject : ObserverSubject<StatusFloatValueObEvent>
    {

        StatusFloatCC statusFloatCC;
        [SerializeField] string StatusToObserve = "";

        void Awake()
        {
            statusFloatCC = GetComponent<StatusFloatCC>();

            foreach (var kvp in statusFloatCC.Statuses) {
                if (kvp.Key == StatusToObserve)
                    kvp.Value.OnChange += OnStatusChange;
            }
        }


        private void OnStatusChange(StatusFloat _status)
        {
            NotifyObservers(new StatusFloatValueObEvent(_status));
        }
    }


}