using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

namespace Laserbean.General.NewSettings
{
    [CreateAssetMenu(fileName = "SettingsObject", menuName = "Laserbean/SettingsObject", order = 0)]
    public class SettingObject : ModuleDataScriptableObject<SettingsComponentData>
    {

    }

    public abstract class SettingsComponentData : ModuleData
    {

    }

    [System.Serializable]
    public class BoolSettingsCompData : SettingsComponentData
    {
        public bool fish; 

    }
}