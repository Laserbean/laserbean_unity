using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Now you could actually also create a concrete interface that 
// visits only a specific type of component, 
// and then register those visitor on an aggregating visitor.


namespace Laserbean.Visitable.Example
{

    public class MyAdvancedVisitor : MonoBehaviour, IVisitor<TestVisitable>
    {
        private Dictionary<Type, object> visitors = new();

        public void RegisterVisitor(IVisitor<TestVisitable> visitor)
        {
            visitors[typeof(TestVisitable)] = visitor;
        }

        public void Visit<T>(T visitable) where T : TestVisitable
        {
            // this is a bit naive, you could easily add support for inheritance
            // e.g. a IVisitor<BaseType> should be callable with a DerivedType
            
            if (!visitors.TryGetValue(typeof(T), out var boxedVisitor)) return;
            if (!(boxedVisitor is IVisitor<T> concreteVisitor)) return; // or throw or log error whatever

            concreteVisitor.Visit(visitable);
        }

        public void Visit(TestVisitable component)
        {
            throw new NotImplementedException();
        }
    }

    public class TestVisitable : MonoBehaviour, IVisitable
    {
        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }


}
