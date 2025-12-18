using System;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.Visitable.Example
{

    public class MyVisitor : MonoBehaviour, IVisitor
    {
        public void Visit(IVisitable visitable)
        {
            // use pattern matching etc.
            if (visitable is MyComponent myComponent)
            {
                // do stuff
            }
        }
    }

}

