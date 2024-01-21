using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.Observers
{
    public interface IObserver
    {
        void UpdateObserver(IObserverEvent observerEvent);
        void UpdateObserver();
    }


    public abstract class Observer<T> : MonoBehaviour, IObserver where T : IObserverEvent
    {
        public abstract void UpdateObserver();

        protected abstract void UpdateObserver(T observerEvent);

        void IObserver.UpdateObserver(IObserverEvent observerEvent)
        {
            if (observerEvent is not T evnt) return;
            UpdateObserver(evnt);
        }
    }


    public interface IObserverSubject
    {
        public List<IObserver> Observers { get; }
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
        void NotifyObservers(IObserverEvent @event);
    }


    public interface IObserverEvent
    {

    }

    // public class Subject
    // {
    //     private List<IObserver> observers = new List<IObserver>();

    //     public void AddObserver(IObserver observer)
    //     {
    //         observers.Add(observer);
    //     }

    //     public void RemoveObserver(IObserver observer)
    //     {
    //         observers.Remove(observer);
    //     }

    //     public void NotifyObservers()
    //     {
    //         foreach (var observer in observers) {
    //             observer.UpdateObserver();
    //         }
    //     }
    // }

}