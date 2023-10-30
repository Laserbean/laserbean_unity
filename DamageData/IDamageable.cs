using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.DamageInterfaces {

public interface IDamageable 
{
    void Damage(DamageData  damage);
    // void Heal(float points);
}


public interface IAttackingEntity {
    bool InRange(Vector2 pos, int attacknum); 

    void StartAttack(float angle, int attacknum); 
}
}
