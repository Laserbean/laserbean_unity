using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper;
using UnityEngine;

namespace Laserbean.General.Observers
{
    public class ObserverSubjectSerialized : MonoBehaviour, IObserverSubject
    {

        public List<IObserver> Observers {
            get {
                return observerObjects.Cast<IObserver>().ToList();
            }
        }

        [SerializeField]
        [RequireInterface(typeof(IObserver))]
        private List<UnityEngine.Object> observerObjects = new();

        public void AddObserver(IObserver observer)
        {
            observerObjects.Add((observer as UnityEngine.Object));
        }

        public void RemoveObserver(IObserver observer)
        {
            observerObjects.Remove((observer as UnityEngine.Object));
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