using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public static class InterfaceExtensions 
// {
// }

public class InterfaceList<T> {
    public List<T> list = new List<T>();


    public void SetList(List<T> _list){
        list = _list; 
    }

    public void Add(T inter) {
        if (!list.Contains(inter)) {
            list.Add(inter); 
        }
    }

    public void Remove(T inter) {
        if (list.Contains(inter)) {
            list.Remove(inter); 
        }
    }

    public int GetValue<U>(System.Func<T, U, int> func, U param) {
        int total = 0; 
        // T instance = System.Activator.CreateInstance<T>();
        foreach(var thing in list) {
            total += func(thing, param);
        }
        return total;
    }
}
