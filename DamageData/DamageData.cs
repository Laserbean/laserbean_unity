using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Laserbean.General.DamageInterfaces {


[System.Serializable]
public class DamageData
{
    public float Amount { get; private set;}
    public WeaponType Weapon_Type{ get; private set;} 
    public DamageType Damage_Type{ get; private set;} 

    public GameObject Source { get; private set;}

    public DamageData(float amount, GameObject source)
    {
        Amount = amount;
        Source = source;
    }

    public void SetAmount(float amount)
    {
        Amount = amount;
    }
}


public enum WeaponType {
    Nothing,
    Melee,
    Bullet,
    Magic
}

public enum DamageType {
    Nothing,
    Physical,
    Infected,
    Fire,
    Water,
    Ice,
    Light,
    Dark,
    Poison,
}


}
