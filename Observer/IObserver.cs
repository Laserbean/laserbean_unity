using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.Observers
{
    public interface IObserver
    {
        void UpdateObserver<T>(T observerEvent);
        void UpdateObserver();

    }


    public interface IObserverSubject
    {
        public List<IObserver> Observers { get; }
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
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