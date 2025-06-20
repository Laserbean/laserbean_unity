using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper;
using UnityEngine;

namespace Laserbean.General.Observers
{
    public class Observable : MonoBehaviour, IObserverable
    {

        public List<IObserver> Observers {
            get {
                return observerObjects.Cast<IObserver>().ToList();
            }
        }

        [SerializeField]
        [RequireInterface(typeof(IObserver))]
        private List<Object> observerObjects = new();

        public void AddObserver(IObserver observer)
        {
            observerObjects.Add(observer as Object);
        }

        public void RemoveObserver(IObserver observer)
        {
            _ = observerObjects.Remove(observer as Object);
        }

        public virtual void NotifyObservers()
        {
            foreach (var observer in Observers) {
                NotifyObserver(observer);
            }
        }

        protected void NotifyObserver(IObserver observer)
        {
            observer.UpdateObserver();
        }

        public virtual void NotifyObservers(IObserverEvent @event)
        {
            foreach (var observer in Observers) {
                observer.UpdateObserver(@event);
            }
        }
    }

    public abstract class ObserverSubject<T_event> : Observable where T_event : IObserverEvent
    {

        public void NotifyObservers(T_event @event)
        {
            base.NotifyObservers(@event);
        }

    }


}