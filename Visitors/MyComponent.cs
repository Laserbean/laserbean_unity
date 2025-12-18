using UnityEngine;

namespace Laserbean.Visitable.Example
{
    public class MyComponent : MonoBehaviour, IVisitable
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this); // Type param is inferred to be MyComponent
        }
    }
}
