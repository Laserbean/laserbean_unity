using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyOrDisable {
    void DestroyOrDisable(); 
    void DestroyOrDisableNextFrame(); 
}
