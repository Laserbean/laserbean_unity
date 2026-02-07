
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Laserbean.General.Follower
{
    // public interface ILocalPositionFollower
    // {
    //     public void SetLocalPositionTarget(Vector3 targetpos);
    // }

    public interface IPosFollower
    {
        public bool HasTarget(FollowTarget target);
        public void AddTarget(FollowTarget target);

        public void RemoveTarget(FollowTarget target);

        public void ClearTargets();
    }

    [System.Serializable]
    public class FollowTargets
    {
        public List<FollowTarget> Targets = new();


        public void AddTarget(FollowTarget target)
        {
            if (!Targets.Contains(target))
            {
                Targets.Add(target);
            }
        }

        public void RemoveTarget(FollowTarget target)
        {
            if (Targets.Contains(target))
            {
                Targets.Remove(target);
            }
        }

        public void ClearTargets()
        {
            Targets.Clear();
        }

        public bool HasTarget(FollowTarget target)
        {
            return Targets.Contains(target);
        }

        public Vector3 GetAveragePos(Vector3 pos)
        {
            // Vector3 averagepos = Vector3.zero;
            // float weights = 0f;
            // foreach (var target in Targets)
            // {
            //     averagepos += (target.Position - pos) * target.PositionWeight;
            //     weights += target.PositionWeight;
            // }
            // averagepos /= weights;
            // return averagepos + pos;
            List<Vector3> positions = Targets.Select(item => item.Position).ToList();
            List<float> weights = Targets.Select(item => item.PositionWeight).ToList();

            return positions.WeightedAverage(weights);
        }

        public Quaternion GetAverageRotation()
        {
            // Short and sweet
            List<Quaternion> rotations = Targets.Select(item => item.Transform.rotation).ToList();
            List<float> weights = Targets.Select(item => item.RotationWeight).ToList();

            return rotations.WeightedAverage(weights);
        }

        internal bool TryAddTarget(FollowTarget mousetarget)
        {
            if (!HasTarget(mousetarget))
            {
                AddTarget(mousetarget);
                return true;
            }
            return false;
        }

        internal bool HasTargets()
        {
            return Targets.Count > 0;
        }
    }


    [System.Serializable]
    public class FollowTarget
    {
        public float PositionWeight;
        public float RotationWeight;
        Vector3 pos;
        public Vector3 Position
        {
            get
            {
                if (Transform == null)
                {
                    return pos;
                }
                else
                {
                    return Transform.position;
                }
            }
            set
            {
                if (Transform == null)
                {
                    pos = value;
                }
                else
                {
                    Debug.LogError("this seems weird");
                    Transform.position = value;
                }
            }
        }
        public Transform Transform;

        public FollowTarget(Vector3 _pos, float posweight = 1f, float rotweight = 1f)
        {
            pos = _pos;
            PositionWeight = posweight;
            RotationWeight = rotweight;
        }

        public FollowTarget(Transform _trans, float posweight = 1f, float rotweight = 1f)
        {
            Transform = _trans;
            PositionWeight = posweight;
            RotationWeight = rotweight;
        }

    }

}