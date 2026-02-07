
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Laserbean.General.Follower
{
    // public interface ILocalPositionFollower
    // {
    //     public void SetLocalPositionTarget(Vector3 targetpos);
    // }

    public interface IPosFollower
    {
        public bool HasTarget(PosFollowTarget target);
        public void AddTarget(PosFollowTarget target);

        public void RemoveTarget(PosFollowTarget target);

        public void ClearTargets();
    }

    [System.Serializable]
    public class PosFollowTargets
    {
        public List<PosFollowTarget> Targets = new();

        public void AddTarget(PosFollowTarget target)
        {
            if (!Targets.Contains(target))
            {
                Targets.Add(target);
            }
        }

        public void RemoveTarget(PosFollowTarget target)
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

        public bool HasTarget(PosFollowTarget target)
        {
            return Targets.Contains(target);
        }

        public Vector3 GetAveragePos(Vector3 pos)
        {
            Vector3 averagepos = Vector3.zero;
            float weights = 0f;
            foreach (var target in Targets)
            {
                averagepos += (target.Position - pos) * target.Weight;
                weights += target.Weight;
            }
            averagepos /= weights;
            return averagepos + pos;
        }

        internal bool TryAddTarget(PosFollowTarget mousetarget)
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
    public class PosFollowTarget
    {
        public float Weight;
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

        public PosFollowTarget(Vector3 _pos, float strength = 1f)
        {
            pos = _pos;
            Weight = strength;
        }

        public PosFollowTarget(Transform _trans, float strength = 1f)
        {
            Transform = _trans;
            Weight = strength;
        }

    }

}