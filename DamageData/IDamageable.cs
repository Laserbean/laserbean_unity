using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.DamageInterfaces {

public interface IDamageable 
{
    void Damage(DamageData  damage);
    // void Heal(float points);
}

}
