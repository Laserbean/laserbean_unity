using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable 
{
    void Damage(int  damage);
    // void Heal(float points);
}


public interface IHealable 
{
    // void Damage(float damage);
    void Heal(int  points);
}

public interface IKnockbackable
{
    void Knockback(Vector2 dir); 
}


public interface IStunnable
{
    void Stun(float time); 
}


