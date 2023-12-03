using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General;

using Pathfinding;
using System;
using UnityEditor;


namespace Laserbean.General
{
    public class AIPathFollower : AIBase, IAstarAI
    {


        float IAstarAI.radius { get => radius; set => radius = value; }

        // [SerializeField] float height;
        float IAstarAI.height { get => height; set => height = value; }


        [SerializeField] float pickNextWayPointDist = 2f;

        Vector3 IAstarAI.position => this.transform.position;


        public float rotationSpeed;

        float IAstarAI.maxSpeed { get => maxSpeed; set => maxSpeed = value; }


        public new Vector3 velocity => rgbd2d.velocity;

        // [SerializeField] float acceleration = 0f; 
        // public bool slowWhenNotFacingTarget = true;


        Vector3 _desiredVelocity;
        public new Vector3 desiredVelocity => _desiredVelocity;

        float IAstarAI.remainingDistance => cur_path.vectorPath.Count - curSeekerIndex;
        bool IAstarAI.reachedDestination => (this.transform.position - destination).sqrMagnitude < 0.05f;


        bool IAstarAI.reachedEndOfPath => curSeekerIndex >= cur_path.vectorPath.Count;

        Vector3 IAstarAI.destination { get => destination; set => destination = value; }
        bool IAstarAI.canSearch { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        bool IAstarAI.canMove { get => canMove; set => canMove = value; }


        bool IAstarAI.hasPath => cur_path != null;

        bool pathPending = false;
        bool IAstarAI.pathPending => pathPending;

        bool IAstarAI.isStopped { get => isStopped; set => isStopped = value; }


        Vector3 _steeringTarget;
        public Vector3 steeringTarget => _steeringTarget;

        Action IAstarAI.onSearchPath { get => onSearchPath; set => onSearchPath = value; }

        Quaternion _rotationTarget;




        Rigidbody2D rgbd2d;
        protected override void Awake()
        {
            base.Awake();
            seeker = GetComponent<Seeker>();
            if (seeker == null) seeker = gameObject.AddComponent<Seeker>();
            rgbd2d = GetComponent<Rigidbody2D>();
            InvokeRepeating("SearchPath", 0.1f, 0.5f);
        }

        [Min(0)]
        [SerializeField] int frameskips = 0;
        int curframe = 0;
        protected override void FixedUpdate()
        {
            // base.FixedUpdate(); 
            if (curframe > frameskips) curframe = 0;
            if (curframe++ > 0) return;

            if (!canMove) return;
            MovementUpdate(Time.fixedDeltaTime * (1 + frameskips), out _steeringTarget, out _rotationTarget);
            FinalizeMovement(_steeringTarget, _rotationTarget);
        }

        public void GetRemainingPath(List<Vector3> buffer, out bool stale)
        {
            throw new NotImplementedException();
        }


        public override void SearchPath()
        {
            if (!this.enabled) return; 
            if (!gameObject.activeInHierarchy) return; 
            pathPending = true;
            seeker.StartPath(this.transform.position, destination, OnPathComplete);
            curSeekerIndex = 0;
        }

        Path cur_path;
        // void OnPathComplete(Path p)
        protected override void OnPathComplete(Path p)

        {
            pathPending = false;

            cur_path = p;

            onSearchPath?.Invoke();
        }


        int curSeekerIndex = 0;



        public override void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation)
        {
            if (isStopped) return;
            rgbd2d.AddForce((nextPosition - this.transform.position), ForceMode2D.Impulse);

            rgbd2d.MoveRotation(nextRotation);
        }

        protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
        {
            nextPosition = this.transform.position;
            nextRotation = this.transform.rotation;

            if (cur_path == null) return;

            if (curSeekerIndex >= cur_path.vectorPath.Count) {
                cur_path = null;
                return;
            }

            if (Vector2.Distance(this.transform.position, cur_path.vectorPath[curSeekerIndex]) <= pickNextWayPointDist) {
                curSeekerIndex++;

                if (curSeekerIndex >= cur_path.vectorPath.Count) {
                    cur_path = null;
                    return;
                }
            }


            Vector3 targetpos = cur_path.vectorPath[curSeekerIndex];
            nextPosition = targetpos;

            nextPosition = ((nextPosition - this.transform.position).normalized * maxSpeed * deltaTime) + this.transform.position;

            Vector2 fish = Vector2.zero;
            fish.x = this.transform.rotation.eulerAngles.x;
            fish.y = this.transform.rotation.eulerAngles.y;

            float targetangle = (targetpos - this.transform.position).VectorToAngle();
            float curangle = this.transform.rotation.eulerAngles.z;


            nextRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(fish.x, fish.y, targetangle), rotationSpeed * deltaTime);


        }

        protected override void ClearPath()
        {
            CancelCurrentPathRequest();
            // cur_path?.Release(this);
			// if (cur_path != null) cur_path.Release(this);

            cur_path = null;
            // interpolator.SetPath(null);
            // reachedEndOfPath = false;

        }
    }



    public class SeekerInstance
    {

        Seeker seeker;
        Path path;

        public float nextWayPointDistance = 0.5f;

        public SeekerInstance(Seeker _seeker)
        {
            seeker = _seeker;
        }

        public void StartPath(Vector3 start, Vector3 end)
        {
            seeker.StartPath(start, end, OnPathComplete);
            curSeekerIndex = 0;
        }

        void OnPathComplete(Path p)
        {
            path = p;
        }


        public void Disable()
        {
            seeker.pathCallback -= OnPathComplete;
        }

        int curSeekerIndex = 0;

        public void UpdateWayPoint(Vector3 position)
        {
            if (path == null || curSeekerIndex >= path.vectorPath.Count) return;
            if (Vector2.Distance(position, path.vectorPath[curSeekerIndex]) <= nextWayPointDistance) {
                curSeekerIndex++;
            }


        }

        public bool IsStillPathing {
            get {
                return path != null && curSeekerIndex < path.vectorPath.Count;
            }
        }

        public Vector3 GetNextWayPoint()
        {
            if (path != null) {
                return path.vectorPath[curSeekerIndex];
            }

            Debug.LogError("This shouldn't happen");
            throw new System.Exception("this shouldn't happen");
        }


    }

#if UNITY_EDITOR
    [CustomEditor(typeof(AIPathFollower))]
    public class YourInheritedClassEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            // Your custom inspector code for the inherited class, if any
        }
    }

#endif
}