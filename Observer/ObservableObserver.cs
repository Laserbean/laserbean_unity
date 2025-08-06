using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Laserbean.General.Observers
{
    public abstract class ObservableObserver<T> : Observable, IObserver where T : IObserverEvent
    {
        public abstract void UpdateObserver();

        protected abstract void UpdateObserver(T observerEvent);

        void IObserver.UpdateObserver(IObserverEvent observerEvent)
        {
            if (observerEvent is not T evnt) return;
            UpdateObserver(evnt);
        }
    }
}
