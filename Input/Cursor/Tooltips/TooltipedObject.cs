using System;
using UnityEngine;

public class TooltipedObject : MonoBehaviour, IToolTipable
{
    [SerializeField] string tooltip; 
    public string GetToolTip()
    {
        return tooltip; 
    }

}
