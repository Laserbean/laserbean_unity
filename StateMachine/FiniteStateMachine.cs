using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.HFiniteStateMachine {

public class FiniteStateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.OnEnter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}

}

