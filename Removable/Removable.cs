using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.Removable
{
    public class Removable : MonoBehaviour
    {
        public void RemoveFromParent()
        {
            GetComponentInParent<IRemovable>()?.Remove();
        }

        public void RemoveFromChildren()
        {
            GetComponentInChildren<IRemovable>()?.Remove();
        }

        public void Remove()
        {
            GetComponent<IRemovable>()?.Remove();
        }
    }
}
