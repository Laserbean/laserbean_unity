using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

using System;


namespace Laserbean.General.NewSettings
{
    [CreateAssetMenu(fileName = "SettingsObject", menuName = "Laserbean/SettingsObject", order = 0)]
    public class SettingObject : ComponentDataBaseScriptableObject<SettingsComponentData>
    {

    }

    public abstract class SettingsComponentData : ComponentDataBase
    {
        public abstract Type GetSettingType(); 
    }

    public abstract class SettingData {

    }


    public class BoolSettingData {
        public bool fish; 
    }

    [Serializable]
    public class BoolSettingsCompData : SettingsComponentData
    {
        public bool fish; 

        public override Type GetSettingType()
        {
            return typeof(BoolSettingData);
        }
    }
}