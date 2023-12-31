using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laserbean.General.OtherInterfaces 
{

    public interface IDamageableInt 
    {
        void Damage(int  damage);
        // void Heal(float points);
    }


    public interface IHealth
    {
        int Health { get; }
        int MaxHealth { get; }
        void SetHealth ( int value );
        void AddHealth ( int change );
    }

    public interface IKnockbackable
    {
        void Knockback(Vector2 dir); 
    }

    public interface IStunnable
    {
        void Stun(float time); 
    }


}
