using UnityEngine;


namespace Laserbean.CoreSystem {

    public abstract class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected Core core;

        protected virtual void Awake()
        {
            if (transform.parent != null) {
                core = transform.parent.GetComponent<Core>();
            } else {
                core = transform.GetComponent<Core>();
            }


            if(core == null) { 
                Debug.LogError("There is no Core on the parent" + gameObject); 
            }
            core.AddComponent(this);
        }

        protected virtual void Start()
        {
        }


        public virtual void LogicUpdate() { }

    }


    public interface ILogicUpdate
    {
        void LogicUpdate();
    }

}

