using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class FloatModifiable : ISerializationCallbackReceiver
{
    [SerializeField] protected List<FloatModifier> Modifiers = new();

    public float BaseValue = 1f;
    public float Value { get; private set; }
    [ShowOnly]
    [SerializeField] float _Value;

    public FloatModifiable()
    {
        Value = BaseValue;
    }
    public virtual void AddModifier(FloatModifier modi)
    {
        if (Modifiers.Contains(modi)) return;
        if (Modifiers.Count >= 1)
        {
            Modifiers[Modifiers.Count - 1].SetNext(modi);
        }
        Modifiers.Add(modi);
        UpdateValue();
    }

    public virtual void RemoveModifier(FloatModifier modi)
    {
        if (!Modifiers.Contains(modi)) return;
        if (Modifiers.Count > 1)
        {
            var ind = Modifiers.IndexOf(modi);
            if (ind >= 1)
                Modifiers[ind - 1].PopNext();
            Modifiers.Remove(modi);
        }
        else
        {
            Modifiers.Remove(modi);
        }
        modi.SetNext(null);
        UpdateValue();

    }

    [EasyButtons.Button]
    protected void UpdateValue()
    {
        if (Modifiers.Count != 0)
        {
            Value = Modifiers[0].GetValue(BaseValue, Modifiers.Count);

            _Value = Value;
            return;
        }
        Value = BaseValue;
        _Value = Value;
    }

    public void OnBeforeSerialize()
    {
        // UpdateValue();
    }

    public void OnAfterDeserialize()
    {
        UpdateValue();
    }
}



[System.Serializable]
public class FloatSequenceModifiable : FloatModifiable
{

    public FloatSequenceModifiable() : base()
    {
    }

    public override void AddModifier(FloatModifier modi)
    {
        if (Modifiers.Contains(modi)) return;
        if (Modifiers.Count >= 1)
        {
            for (int i = 0; i < Modifiers.Count; i++)
            {
                if (modi.Priority <= Modifiers[i].Priority)
                {
                    if (i > 0)
                    {
                        Modifiers[i - 1].SetNext(modi);
                    }
                    modi.SetNext(Modifiers[i]);
                    Modifiers.Insert(i, modi);
                    UpdateValue();
                    return;
                }
            }

            Modifiers.Add(modi);
            Modifiers[Modifiers.Count - 2].SetNext(modi);
        }
        else
        {
            Modifiers.Add(modi);
        }
        UpdateValue();
    }


}