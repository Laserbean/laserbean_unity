using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General;

using System;
#if PATHFINDING
using Pathfinding;


public class TestAIPath : AIBase, IAstarAI
{
    float IAstarAI.radius { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    float IAstarAI.height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    float IAstarAI.maxSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    float IAstarAI.remainingDistance => throw new NotImplementedException();
    bool IAstarAI.reachedDestination => throw new NotImplementedException();
    bool IAstarAI.reachedEndOfPath => throw new NotImplementedException();
    bool IAstarAI.canMove { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    bool IAstarAI.hasPath => throw new NotImplementedException();
    bool IAstarAI.pathPending => throw new NotImplementedException();

    Vector3 IAstarAI.steeringTarget => throw new NotImplementedException();

    protected override void ClearPath()
    {
        throw new NotImplementedException();
    }

    protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
    {
        throw new NotImplementedException();
    }

    protected override void OnPathComplete(Path newPath)
    {
        throw new NotImplementedException();
    }

    void IAstarAI.GetRemainingPath(List<Vector3> buffer, out bool stale)
    {
        throw new NotImplementedException();
    }
}

#endif