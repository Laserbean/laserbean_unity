
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Unity.Mathematics;
using UnityEngine;

namespace Laserbean.SpecialData
{
[System.Serializable]
public class SpecialDict : ICollection<KeyValuePair<string, SpecialData>>, IEnumerable<KeyValuePair<string, SpecialData>>, 
    // IEnumerable, 
    IDictionary<string, SpecialData>, IReadOnlyCollection<KeyValuePair<string, SpecialData>>, 
    IReadOnlyDictionary<string, SpecialData>, 
    // ICollection, 
    // IDictionary, 
    IDeserializationCallback, 
    // ISerializable,
    ISerializationCallbackReceiver

{

    [System.Serializable]
    public class SpecialKVP
    {
        public string Key;
        public SpecialData Value;

        public SpecialKVP(string kkk, SpecialData sss) {
            Key = kkk; Value = sss; 
        }
    }

    [NonSerialized]
    // [SerializeField]
    List<string> Keys = new List<string>();
    // [NonSerialized]
    // List<SpecialData> Values = new List<SpecialData>();

    public List<SpecialKVP> KVPs = new List<SpecialKVP>(); 


    public SpecialDict () {
        // Keys = new List<string>();
        // Values = new List<SpecialData>();
        KVPs = new List<SpecialKVP>(); 
    }

    public SpecialData this [string key] {
        get {
            if (KVPs.Count != Keys.Count) ResetKeys(); 
            return KVPs[Keys.IndexOf(key)].Value; 
        }
        set {
            if (Keys.IndexOf(key) >= 0) {
                KVPs[Keys.IndexOf(key)].Value = value; 
                KVPs[Keys.IndexOf(key)].Key = key; 

            } else {
                Keys.Add(key);
                KVPs.Add(new SpecialKVP(key, value)); 
            }
        }
    }

    SpecialData IDictionary<string, SpecialData>.this[string key] { 
        get => KVPs[Keys.IndexOf(key)].Value; 
        set => KVPs[Keys.IndexOf(key)].Value = value; 
    }

    SpecialData IReadOnlyDictionary<string, SpecialData>.this[string key] => KVPs[Keys.IndexOf(key)].Value;

    int ICollection<KeyValuePair<string, SpecialData>>.Count => Keys.Count;

    int IReadOnlyCollection<KeyValuePair<string, SpecialData>>.Count => Keys.Count;

    bool ICollection<KeyValuePair<string, SpecialData>>.IsReadOnly => false;

    ICollection<string> IDictionary<string, SpecialData>.Keys => Keys;

    IEnumerable<string> IReadOnlyDictionary<string, SpecialData>.Keys => Keys;

    ICollection<SpecialData> IDictionary<string, SpecialData>.Values  { 
        get {
            List<SpecialData> valuesList = new List<SpecialData>();
            foreach (SpecialKVP kvp in KVPs)
            {
                valuesList.Add(kvp.Value);
            }
            return valuesList;

        }
    }

    IEnumerable<SpecialData> IReadOnlyDictionary<string, SpecialData>.Values { 
        get {
            List<SpecialData> valuesList = new List<SpecialData>();
            foreach (SpecialKVP kvp in KVPs)
            {
                valuesList.Add(kvp.Value);
            }
            return valuesList;

        }
    }

    // public void AddSpecial(string key, float num) {

    // }

    public bool TryAdd(string key, SpecialData value) {
        if (Keys.IndexOf(key) >= 0) 
            return false;

        if (key == null) 
            return false;

        Keys.Add(key); 
        KVPs.Add(new SpecialKVP(key, value));
        return true; 
    }

    public void Add(string key, SpecialData value) {
        if (Keys.IndexOf(key) >= 0) 
            throw new System.ArgumentException("Key " + key + " already exists");

        if (key == null) 
            throw new System.ArgumentNullException("Key is null");

        Keys.Add(key); 
        KVPs.Add(new SpecialKVP(key, value));
    }

    void ICollection<KeyValuePair<string, SpecialData>>.Add(KeyValuePair<string, SpecialData> item)
    {
        if (Keys.IndexOf(item.Key) >= 0) 
            throw new System.ArgumentException("Key " + item.Key + " already exists");

        if (item.Key == null) 
            throw new System.ArgumentNullException("Key is null");

        Keys.Add(item.Key); 
        // Values.Add(item.Value);
        KVPs.Add(new SpecialKVP(item.Key, item.Value));

    }

    void IDictionary<string, SpecialData>.Add(string key, SpecialData value)
    {
        Add(key, value); 
    }

    void ICollection<KeyValuePair<string, SpecialData>>.Clear()
    {
        Keys.Clear(); 
        KVPs.Clear(); 

    }

    public void Clear() {
        Keys.Clear(); 
        KVPs.Clear(); 
    }

    public bool Contains(string key) {
        return Keys.Contains(key);
    }

    bool ICollection<KeyValuePair<string, SpecialData>>.Contains(KeyValuePair<string, SpecialData> item)
    {
        return Keys.Contains(item.Key);
    }

    bool IDictionary<string, SpecialData>.ContainsKey(string key)
    {
        return Keys.Contains(key);
    }

    bool IReadOnlyDictionary<string, SpecialData>.ContainsKey(string key)
    {
        return Keys.Contains(key);
    }

        void ICollection<KeyValuePair<string, SpecialData>>.CopyTo(KeyValuePair<string, SpecialData>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

    IEnumerator<KeyValuePair<string, SpecialData>> IEnumerable<KeyValuePair<string, SpecialData>>.GetEnumerator()
    {
        for (int i = 0; i < KVPs.Count; i++) {
            yield return new KeyValuePair<string, SpecialData>(Keys[i], KVPs[i].Value);
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        for (int i = 0; i < KVPs.Count; i++) {
            yield return KVPs[i].Value;
        }
    }

        // void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        // {
        //     // throw new System.NotImplementedException();
        //     info =
        // }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            // throw new System.NotImplementedException();
            Keys = new List<string>(); 
            ResetKeys();
        }


    public bool Remove(string key) {
        int index = Keys.IndexOf(key); 

        if (index < 0) return false; 

        Keys.RemoveAt(index);
        KVPs.RemoveAt(index); 

        return true; 
    }

    bool ICollection<KeyValuePair<string, SpecialData>>.Remove(KeyValuePair<string, SpecialData> item)
    {
        int index = Keys.IndexOf(item.Key); 

        if (index < 0) return false; 

        Keys.RemoveAt(index);
        KVPs.RemoveAt(index); 

        return true; 
    }

    bool IDictionary<string, SpecialData>.Remove(string key)
    {
        return Remove(key); 
    }

    public bool TryGetValue(string key, out SpecialData value) {
        value = new SpecialData(); 

        int index = Keys.IndexOf(key); 

        if (index < 0) return false; 

        value = KVPs[index].Value; 

        return true; 
    }


    bool IDictionary<string, SpecialData>.TryGetValue(string key, out SpecialData value)
    {
        return TryGetValue(key, out value); 
    }

    bool IReadOnlyDictionary<string, SpecialData>.TryGetValue(string key, out SpecialData value)
    {
        return TryGetValue(key, out value); 
    }

    void ResetKeys() {
        if (KVPs.Count != Keys.Count) {
            Keys.Clear(); 

            foreach(var KVP in KVPs) {
                Keys.Add(KVP.Key); 
            }
        } else {
            for(int i = 0; i< KVPs.Count; i++) {
                Keys[i] = KVPs[i].Key; 
            }
        }
    }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            ResetKeys();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            ResetKeys();
        }
    }

[System.Serializable]
public struct SpecialData {
    public int Int;
    public float Float;

    [SerializeField]
    private float3 Float3;

    public string String; 

    public Vector3 Vector3 {
        get => new Vector3(Float3.x, Float3.y, Float3.z);
        set => Float3 = new float3(value.x, value.y, value.z); 

    }

    public Vector2 Vector2 {
        get => new Vector2(Float3.x, Float3.y);
        set => Float3 = new float3(value.x, value.y, 0); 

    }
}
 

    [System.Serializable]
    public class StringFloatDictionary : ICollection<KeyValuePair<string, float>>, IEnumerable<KeyValuePair<string, float>>,
        // IEnumerable, 
        IDictionary<string, float>, IReadOnlyCollection<KeyValuePair<string, float>>, IReadOnlyDictionary<string, float>,
        // ICollection, 
        // IDictionary, 
        IDeserializationCallback, // ISerializable, 
        ISerializationCallbackReceiver

    {
        public List<string> keys; 
        public List<float> values; 
        public ICollection<string> Keys => keys;
        public ICollection<float> Values => values;

        public float this[string key] { 
            get => values[keys.IndexOf(key)]; 
            set {
                if (keys.Contains(key)){
                    values[keys.IndexOf(key)] = value;
                } else {
                    values.Add(value);
                    keys.Add(key);
                }
            }
        }

        public int Count => values.Count == keys.Count ? values.Count : throw new SystemException("keys and values have different lenght");

        public bool IsReadOnly => false;


        IEnumerable<string> IReadOnlyDictionary<string, float>.Keys => throw new NotImplementedException();
        IEnumerable<float> IReadOnlyDictionary<string, float>.Values => throw new NotImplementedException();

        public void Add(KeyValuePair<string, float> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(string key, float value)
        {
            if (Keys.Contains(key)) throw new ArgumentException(); 
            if (key == null) throw new ArgumentNullException(); 

            Keys.Add(key);
            values.Add(value); 
        }

        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }

        public bool Contains(KeyValuePair<string, float> item)
        {
            return keys.Contains(item.Key) && values.Contains(item.Value); 
        }

        public bool ContainsKey(string key)
        {
            return keys.Contains(key);
        }

            public void CopyTo(KeyValuePair<string, float>[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

        public IEnumerator<KeyValuePair<string, float>> GetEnumerator()
        {
            for (int i = 0; i < Count; i++) {
                yield return new KeyValuePair<string, float>(keys[i], values[i]);
            }
        }

            public void OnAfterDeserialize()
            {
                
            }

            public void OnBeforeSerialize()
            {
                
            }

            public void OnDeserialization(object sender)
            {
                
            }

        public bool Remove(KeyValuePair<string, float> item)
        {
            int index = keys.IndexOf(item.Key); 

            if (index < 0) return false; 

            keys.RemoveAt(index);
            values.RemoveAt(index); 

            return true; 
        }

        public bool Remove(string key)
        {
            int index = keys.IndexOf(key); 

            if (index < 0) return false; 

            keys.RemoveAt(index);
            values.RemoveAt(index); 

            return true; 
        }

        public bool TryGetValue(string key, out float value)
        {
            value = 0f; 
            if (!keys.Contains(key)) 
                return false; 
            
            value = values[keys.IndexOf(key)]; 
            return true; 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; i++) {
                yield return new KeyValuePair<string, float>(keys[i], values[i]);
            }        
        }
    }


}