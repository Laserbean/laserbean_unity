using System;
using System.Collections.Generic;
using UnityEngine;

public interface IVisitable
{
    void Accept(IVisitor visitor);
}

// public interface IVisitableGeneric
// {
//     void Accept(IVisitorGeneric visitor);
// }
