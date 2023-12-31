using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.Observers
{
    public abstract class ObserverSubject : MonoBehaviour, IObserverSubject
    {

        private List<IObserver> observers = new List<IObserver>();

        [HideInInspector]
        public List<IObserver> Observers => observers;

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public virtual void NotifyObservers()
        {
            foreach (var observer in Observers) {
                NotifyObserver(observer);
            }
        }

        protected virtual void NotifyObserver(IObserver observer)
        {
            observer.UpdateObserver();

        }
    }

}