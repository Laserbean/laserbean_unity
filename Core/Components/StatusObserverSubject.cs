using System;
using System.Collections;
using System.Collections.Generic;
using Laserbean.General.Observers;
using UnityEngine;

namespace Laserbean.CoreSystem.BasicComponents
{
    [RequireComponent(typeof(StatusFloatCC))]
    public class StatusObserverSubject : ObserverSubjectSerialized
    {

        StatusFloatCC statusFloatCC;
        void Awake()
        {
            statusFloatCC = this.GetComponent<StatusFloatCC>();

            foreach (var kvp in statusFloatCC.Statuses) {
                if (kvp.Key == StatusToObserve)
                    kvp.Value.OnChange += OnStatusChange;
            }
        }
        [SerializeField] string StatusToObserve = "";

        private void OnStatusChange(StatusFloat _status)
        {
            status = _status;
            NotifyObservers();
        }

        StatusFloat status = null;
        public override void NotifyObservers()
        {
            if (status == null) return;
            base.NotifyObservers();
            status = null;
        }


        protected override void NotifyObserver(IObserver observer)
        {
            observer.UpdateObserver(new StatusFloatValue(status));
        }

    }


}