// Based no Bardent.ModifierSystem; 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Laserbean.BardentModifierSystem
{

    /*
     * The Modifiers class is a generic class for holding and acting upon a list of Modifiers of a specific type. It also allows us to
     * easily apply all modifiers to a value. An example of where this is used can be seen in the DamageReceiver Core Component.
     */
    [System.Serializable]
    public class Modifiers<TModifierType, TValueType> where  TModifierType: Modifier<TValueType>
    {
        private readonly List<TModifierType> modifierList = new ();

        [SerializeField] List<ModifierDisplay> Modifiers_List = new (); 

        
        // Runs through the modifierList and applies each modifier to the input value. Note that the output of the first modifier is used as the input of the next
        // modifier. This is not a smart system but works for our use case. Better systems might allow modifiers to be sorted first based on some property.
        public TValueType ApplyAllModifiers(TValueType initialValue) {
            var modifiedValue = initialValue;

            foreach (var modifier in modifierList) {
                modifiedValue = modifier.ModifyValue(modifiedValue);
            }

            return modifiedValue;
        }

        public void AddModifier(TModifierType modifier)    {
            modifierList.Add(modifier);
            Modifiers_List.Add(new (modifier));
        }
        public void RemoveModifier(TModifierType modifier) {
            modifierList.Remove(modifier);
            Modifiers_List.Remove(new (modifier));
        }
    }

    /*
     * The base abstract Modifier class is used so that we can store all modifiers in a list together so that we can iterate though them
     */
    [System.Serializable]
    public abstract class Modifier
    {
        public string Name {get; protected set;}

        public Modifier() {
            Name = "Modifier";
        }

        public abstract string GetInfo();
    }

    [System.Serializable]
    public class ModifierDisplay {
        [field: SerializeField, ShowOnly]
        public string Name {get; private set;}
        [field: SerializeField, ShowOnly]
        public string Info {get; private set;}

        public ModifierDisplay(Modifier modifier) {
            Name = modifier.Name; 
            Info = modifier.GetInfo(); 
        } 
    }


    /*
     * The Generic Base abstract Modifier class allows us to define the type that is being modified by the modifier. Most modifiers will inherit from this or a child
     * class that already specifies the type T.
     */
    [System.Serializable]
    public abstract class Modifier<T> : Modifier
    {
        public abstract T ModifyValue(T value);

        public Modifier() : base() {
            Name = "Modifier" +  typeof(T);
        }

        public abstract override string GetInfo();
    }
}