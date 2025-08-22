
using System;
using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Laserbean.CustomGUI
{

    [System.Serializable]
    public class GUI_Window_Info
    {
        public Vector3 Position;
        public Vector3 Scale = Vector3.one;

        [Range(0f, 1f)]
        public float Opacity;


        public bool Interactable;
        public bool BlocksRaycast;
    }
}