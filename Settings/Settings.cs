
using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

using System;
using Laserbean.SpecialData;



namespace Laserbean.General.NewSettings
{
    [Serializable]
    public class Settings
    {
        public CustomDictionary<string, SettingData> Data = new();
    }
}