using UnityEngine;

public enum ModifierType
{
    None = 0,
    Add = 1,
    Multiply = 2,
    Override = 3,
}


[System.Serializable]
public class FloatModifier : ISerializationCallbackReceiver
{
    // [SerializeField] string Name = "";
    [SerializeField] float value;
    FloatModifier next = null;

    // [SerializeField, ShowOnly] string NextName = "";

    [SerializeField] ModifierType type;

    public int Priority = -1;
    public FloatModifier()
    {
    }
    
    public FloatModifier(FloatModifier copy)
    {
        // Name = copy.Name + " copy";
        value = copy.value;
        type = copy.type;

        Priority = copy.Priority;
    }

    public void SetNext(FloatModifier nextModi)
    {
        next = nextModi;
    }
    public FloatModifier GetNext()
    {
        return next;
    }
    public FloatModifier PopNext()
    {
        var tempnext = next;
        if (next == null)
        {
            return null;
        }
        if (next.next != null)
        {
            next = next.next;
        }
        else
        {
            next = null;
        }
        return tempnext;
    }

    // public void InsertNext(FloatModifier nextModi)
    // {
    //     nextModi.next = next;

    //     next = nextModi;
    // }

    public float GetValue(float prev_value, int safetyValue)
    {
        safetyValue -= 1;
        if (safetyValue < 0)
        {
            Debug.LogError("Not sure if this is supposed to be here");
            return prev_value;

        }
        var returnval = prev_value;
        switch (type)
        {
            case ModifierType.Add:
                returnval += value;
                break;

            case ModifierType.Multiply:
                returnval *= value;
                break;
            case ModifierType.Override:
                returnval = value;
                break;
        }
        if (next == null)
        {
            return returnval;
        }
        else
        {
            return next.GetValue(returnval, safetyValue);
        }
    }
    public float GetBaseValue()
    {
        return value;
    }

    public void OnBeforeSerialize()
    {
        // // throw new System.NotImplementedException();
        // if (next != null)
        // {
        //     NextName = next.Name;

        // }
        // else
        // {
        //     NextName = "";
        // }
    }

    public void OnAfterDeserialize()
    {
        // throw new System.NotImplementedException();
    }
}


