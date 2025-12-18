using System;
using System.Collections.Generic;
using UnityEngine;

public interface IVisitable
{
    void Accept(IVisitor visitor);
}
