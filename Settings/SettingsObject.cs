using System.Collections;
using System.Collections.Generic;
using Laserbean.General.GenericStuff;
using UnityEngine;

using System;
using System.Linq;


namespace Laserbean.General.NewSettings
{
    [CreateAssetMenu(fileName = "SettingsObject", menuName = "Laserbean/SettingsObject", order = 0)]
    public class SettingsObject : ComponentDataBaseScriptableObject<SettingsComponentData>
    {

        private void OnValidate() {
            foreach(SettingsComponentData fish in ComponentData.Cast<SettingsComponentData>()) {
                if (fish.GetValueName() != fish.Name) 
                    fish.SetValueName(fish.Name); 
            }
        }
    }

}