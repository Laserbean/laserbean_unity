using UnityEngine;
using UnityEngine.Events;

public class DoOnce : MonoBehaviour
{
    bool hasDone = false;

    [SerializeField] UnityEvent DoSomethingEvent;

    public void DoSomething()
    {
        if (hasDone) return;
        DoSomethingEvent?.Invoke();
        hasDone = true;
    }
}

