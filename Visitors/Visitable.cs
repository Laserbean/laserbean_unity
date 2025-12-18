using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Laserbean.Items
{
    public class Visitable : MonoBehaviour, IVisitable
    {
        List<IVisitable> itemCollectors = new();

        void Start()
        {
            itemCollectors = gameObject.GetComponentsInChildren<IVisitable>().ToList();
            itemCollectors.Remove(this);
        }
        
        public void Accept(IVisitor visitor)
        {
            foreach (var comp in itemCollectors)
            {
                comp.Accept(visitor);
            }
        }
    }
}
