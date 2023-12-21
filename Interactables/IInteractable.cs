using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Laserbean.General;
using UnityEngine.PlayerLoop;

namespace Laserbean.General.Interactables
{
    public interface IInteractable
    {
        void Interact(GameObject gameObject);
        void Highlight();
        void UnHighlight();
    }
}