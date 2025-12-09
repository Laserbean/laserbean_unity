# State Machine

Created based on the Bardent state machine system. 

An *Entity* is something that uses the state machine, the *FiniteStateMachine*.  

*State*s are used to program different behavious in the state machine. 

## Example usage
```
using Laserbean.HFiniteStateMachine;
using UnityEngine;

public class PlayerEntity2DTD : Entity
{
    // public States {get; private set;}
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    // public Components 
    // Such as core, rigidbody, transforms, colliders, etc. 

    protected override void Awake()
    {
        base.Awake();

        IdleState = new(this, StateMachine, "idle");
        MoveState = new(this, StateMachine, "idle");
    }
    
}


public abstract class PlayerState : State
{
    protected PlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
    }
}

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
    }
}


public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
    }
}