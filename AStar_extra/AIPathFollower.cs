using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General;

using Pathfinding;
using System;

namespace Laserbean.General
{
public class AIPathFollower : MonoBehaviour, IAstarAI
{

    Seeker seeker;

    [SerializeField] float radius; 
    float IAstarAI.radius { get => radius;  set => radius = value; }

    [SerializeField] float height; 
    float IAstarAI.height { get => height;  set => height = value; }


    [SerializeField] float pickNextWayPointDist = 2f; 

    Vector3 IAstarAI.position => this.transform.position;

    
    public Quaternion rotation         { get => this.transform.rotation; set => this.transform.rotation = value; }
    
    public float rotationSpeed; 
    public float maxSpeed; 
    float IAstarAI.maxSpeed              { get => maxSpeed; set => maxSpeed = value; }



    public Vector3 velocity            => rgbd2d.velocity; 

    Vector3 _desiredVelocity; 
    public Vector3 desiredVelocity     => _desiredVelocity;

    float IAstarAI.remainingDistance     => throw new NotImplementedException();
    bool IAstarAI.reachedDestination     => throw new NotImplementedException();
    bool IAstarAI.reachedEndOfPath       => throw new NotImplementedException();

    Vector3 destination;
    Vector3 IAstarAI.destination         { get => destination; set => destination = value; }
    bool IAstarAI.canSearch              { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    bool IAstarAI.canMove                { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    bool IAstarAI.hasPath                => throw new NotImplementedException();
    bool IAstarAI.pathPending            => throw new NotImplementedException();

    bool isStopped = false; 
    bool IAstarAI.isStopped              { get =>isStopped;  set =>isStopped = value ; }
    

    Vector3 _steeringTarget; 
    public Vector3 steeringTarget      => _steeringTarget;

    Action IAstarAI.onSearchPath { get => onSearchPath; set => onSearchPath = value; }

    Quaternion _rotationTarget; 

    
    public Action onSearchPath;


    Rigidbody2D rgbd2d; 
    private void Awake() {
        seeker = this.GetComponent<Seeker>(); 
        rgbd2d = this.GetComponent<Rigidbody2D>(); 
        InvokeRepeating("SearchPath", 0.1f, 0.5f); 
    }

    void repeatthis() {
        SearchPath();
    }


    private void FixedUpdate() {

        MovementUpdate(Time.fixedDeltaTime, out _steeringTarget, out _rotationTarget);

        FinalizeMovement(_steeringTarget, _rotationTarget);
    }

    public void GetRemainingPath(List<Vector3> buffer, out bool stale) {
        throw new NotImplementedException();
    }

    public void SearchPath() {
        seeker.StartPath(this.transform.position, destination, OnPathComplete);
        curSeekerIndex = 0; 

    }

    Path cur_path; 
    void OnPathComplete(Path p) {
        cur_path = p; 

        onSearchPath();
    }

    public void SetPath(Path path) {
        cur_path = path; 
    }

    public void Teleport(Vector3 newPosition, bool clearPath) {
        throw new NotImplementedException();
    }

    public void Move(Vector3 deltaPosition) {
        throw new NotImplementedException();
    }

    int curSeekerIndex = 0;

    public void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation) {
        nextPosition = this.transform.position; 
        nextRotation = this.transform.rotation; 

        if (cur_path == null) return;

        
        if (Vector2.Distance(this.transform.position, cur_path.vectorPath[curSeekerIndex]) <= pickNextWayPointDist) {
            curSeekerIndex++;
        }



        if (curSeekerIndex >= cur_path.vectorPath.Count) return;

        Vector3 targetpos = cur_path.vectorPath[curSeekerIndex];
        nextPosition = targetpos; 

        nextPosition = ((nextPosition - this.transform.position).normalized * maxSpeed * deltaTime) +  this.transform.position;

        Vector2 fish = Vector2.zero; 
        fish.x = this.transform.rotation.eulerAngles.x;
        fish.y = this.transform.rotation.eulerAngles.y;

        float targetangle = (targetpos - this.transform.position).VectorToAngle();
        float curangle = this.transform.rotation.eulerAngles.z; 

        
        nextRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(fish.x,fish.y,targetangle), rotationSpeed * deltaTime);

    
    }

    public void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation) {
        if (isStopped) return; 
        rgbd2d.AddForce((nextPosition - this.transform.position), ForceMode2D.Impulse); 

        rgbd2d.MoveRotation(nextRotation);
    }
}



public class SeekerInstance {

    Seeker seeker;
    Path path; 

    public float nextWayPointDistance = 0.5f; 

    public SeekerInstance (Seeker _seeker){
        seeker = _seeker; 
    }

    public void StartPath(Vector3 start, Vector3 end) {
        seeker.StartPath (start, end, OnPathComplete);
        curSeekerIndex = 0; 
    }

    void OnPathComplete(Path p) {
        path = p; 
    }


    public void Disable() {
        seeker.pathCallback -= OnPathComplete; 
    }

    int curSeekerIndex = 0;

    public void UpdateWayPoint(Vector3 position) {
        if (path == null || curSeekerIndex >= path.vectorPath.Count) return;
        if (Vector2.Distance(position, path.vectorPath[curSeekerIndex]) <= nextWayPointDistance) {
            curSeekerIndex++;
        }
    
        
    }

    public bool IsStillPathing {
        get {
            return  path != null && curSeekerIndex < path.vectorPath.Count; 
        }
    }

    public Vector3 GetNextWayPoint() {
        if (path != null) {
            return path.vectorPath[curSeekerIndex];
        }

        Debug.LogError("This shouldn't happen"); 
        throw new System.Exception("this shouldn't happen"); 
    }


}


}