
using UnityEngine;


namespace Laserbean.General.Follower
{
    public interface IPositionFollower
    {
        public void SetTarget(Vector3 targetpos);
        public void SetTarget(Transform target);

        public void RemoveTargetTransform();

        public void RemoveTargetPosition();
    }

    // public interface ILocalPositionFollower
    // {
    //     public void SetLocalPositionTarget(Vector3 targetpos);
    // }

    public interface IPosFollower
    {
        public void AddTarget(PosFollowTarget target);

        public void RemoveTarget(PosFollowTarget target);

        public void ClearTargets();
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