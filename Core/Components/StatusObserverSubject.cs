using System;
using System.Collections;
using System.Collections.Generic;
using Laserbean.General.Observers;
using UnityEngine;

namespace Laserbean.CoreSystem.BasicComponents
{
    [RequireComponent(typeof(StatusCC))]
    public class StatusObserverSubject : ObserverSubjectSerialized
    {

        public StatusCC statusCC;

        void Awake()
        {
            statusCC = this.GetComponent<StatusCC>();

            foreach (var kvp in statusCC.Statuses) {
                if (kvp.Key == StatusToObserve)
                    kvp.Value.OnChange += OnStatusChange;
            }
        }
        [SerializeField] string StatusToObserve = "";

        private void OnStatusChange(Status _status)
        {
            status = _status;
            NotifyObservers();
        }

        Status status = null;
        public override void NotifyObservers()
        {
            if (status == null) return;
            base.NotifyObservers();
            status = null;
        }


        protected override void NotifyObserver(IObserver observer)
        {
            observer.UpdateObserver(new StatusValue(status));
        }

    }


}