using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.Observers
{
    public interface IObserver
    {
        public void UpdateObserver(IObserverEvent observerEvent);
        public void UpdateObserver();
    }

    public interface IObserver<T> : IObserver where T : IObserverEvent
    {
        void UpdateObserver(T observerEvent);
    }


    public abstract class Observer<T> : MonoBehaviour, IObserver where T : IObserverEvent
    {
        public abstract void UpdateObserver();

        public abstract void UpdateObserver(T observerEvent);

        void IObserver.UpdateObserver(IObserverEvent observerEvent)
        {
            if (observerEvent is not T evnt) return;
            UpdateObserver(evnt);
        }
    }


    public interface IObserverable
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