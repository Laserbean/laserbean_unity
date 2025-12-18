using System;
using System.Collections.Generic;
using UnityEngine;



public interface IVisitor
{
    public void Visit(IVisitable visitable);

}


public interface IVisitor<T> where T : Component, IVisitable
{
    void Visit(T component);
}

public interface IVisitorGeneric
{
    public void Visit<T>(T visitable) where T : Component, IVisitable;
}

