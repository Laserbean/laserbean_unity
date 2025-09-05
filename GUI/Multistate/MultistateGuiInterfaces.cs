using System.Collections;
using System.Collections.Generic;
using Laserbean.General;
using UnityEngine;
using System;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Laserbean.CustomGUI
{

    public interface IGuiManager
    {
        public int GetCurrentState();
        public bool NameExists(string name);
        public int GetStateNumber(string nme);
        public void DoState(int num);
        public void DoStateInstant(int num);
    }

    public interface IGuiObject
    {
        public void ShowGuiAt(int number);
        public void StartGuiLerpAt(int number);
    }
}