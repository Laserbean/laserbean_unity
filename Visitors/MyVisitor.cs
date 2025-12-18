using System;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.Visitable.Example
{

    public class MyVisitor : MonoBehaviour, IVisitorGeneric
    {
        public void Visit<T>(T visitable) where T : Component, IVisitable
        {
            // use pattern matching etc.
            if (visitable is MyComponent myComponent)
            {
                // do stuff
                
            }
        }
    }

}

