


using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Unity.Mathematics;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Laserbean.SpecialData
{

    [System.Serializable]
    public class CustomDictionary <Tkey, Tvalue>: ICollection<KeyValuePair<Tkey, Tvalue>>, IEnumerable<KeyValuePair<Tkey, Tvalue>>,
        // IEnumerable, 
        IDictionary<Tkey, Tvalue>, IReadOnlyCollection<KeyValuePair<Tkey, Tvalue>>, IReadOnlyDictionary<Tkey, Tvalue>,
        // ICollection, 
        // IDictionary, 
        IDeserializationCallback, // ISerializable, 
        ISerializationCallbackReceiver

    {

        public List<SerializedKeyValuePair<Tkey, Tvalue>> keyValueList = new(); 

        [NonSerialized]
        public List<Tkey> keys = new(); 
        [NonSerialized]
        public List<Tvalue> values = new(); 
        
        public ICollection<Tkey> Keys => keys;
        public ICollection<Tvalue> Values => values;

        public Tvalue this[Tkey key] { 
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


        IEnumerable<Tkey> IReadOnlyDictionary<Tkey, Tvalue>.Keys => throw new NotImplementedException();
        IEnumerable<Tvalue> IReadOnlyDictionary<Tkey, Tvalue>.Values => throw new NotImplementedException();

        public void Add(KeyValuePair<Tkey, Tvalue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(Tkey key, Tvalue value)
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

        public bool Contains(KeyValuePair<Tkey, Tvalue> item)
        {
            return keys.Contains(item.Key) && values.Contains(item.Value); 
        }

        public bool ContainsKey(Tkey key)
        {
            return keys.Contains(key);
        }

            public void CopyTo(KeyValuePair<Tkey, Tvalue>[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            for (int i = 0; i < Count; i++) {
                yield return new KeyValuePair<Tkey, Tvalue>(keys[i], values[i]);
            }
        }

        void UpdateKeysAndValues() {
            keys.Clear();
            values.Clear(); 
            foreach(var kvp in keyValueList) {
                if (keys.Contains(kvp.Key))
                    Debug.LogError("Dictionary already contains key: " + kvp.Key);
                
                keys.Add(kvp.Key);
                values.Add(kvp.Value); 
            }
        }
            public void OnAfterDeserialize()
            {
                UpdateKeysAndValues();
            }

            public void OnBeforeSerialize()
            {
                keyValueList.Clear();
                for (int i = 0; i < Count; i++) {
                    keyValueList.Add (new SerializedKeyValuePair<Tkey, Tvalue>(keys[i], values[i]));
                }     

            }

            public void OnDeserialization(object sender)
            {
                UpdateKeysAndValues();
            }

        public bool Remove(KeyValuePair<Tkey, Tvalue> item)
        {
            int index = keys.IndexOf(item.Key); 

            if (index < 0) return false; 

            keys.RemoveAt(index);
            values.RemoveAt(index); 

            return true; 
        }

        public bool Remove(Tkey key)
        {
            int index = keys.IndexOf(key); 

            if (index < 0) return false; 

            keys.RemoveAt(index);
            values.RemoveAt(index); 

            return true; 
        }

        public bool TryGetValue(Tkey key, out Tvalue value)
        { 
            value = default(Tvalue);
            if (!keys.Contains(key)) 
                return false; 
            
            value = values[keys.IndexOf(key)]; 
            return true; 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; i++) {
                yield return new KeyValuePair<Tkey, Tvalue>(keys[i], values[i]);
            }        
        }
    }

[System.Serializable]
public struct SerializedKeyValuePair<TKey, TValue>
{
    [field: SerializeField]
    public TKey Key {set; get; }

    [field: SerializeField]
    public TValue Value {set; get; }

    public SerializedKeyValuePair(TKey key, TValue val) {
        Key = key;
        Value = val; 
    }
}


#if UNITY_EDITOR



[CustomPropertyDrawer(typeof(CustomDictionary<,>))]
public class CustomDictionaryProperty : PropertyDrawer 
{
    const string propname = "keyValueList";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Calculate the height of a single line
        float lineHeight = EditorGUIUtility.singleLineHeight;

        // Draw a label for the list

        // EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);


        SerializedProperty keyValuePairs = property.FindPropertyRelative(propname);


        EditorGUI.PropertyField(position, keyValuePairs, label, true);


        // EditorGUI.LabelField(position, "Fish");
        EditorGUI.EndProperty();

    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float totalheight = 0f; 

        totalheight += lineHeight*1; 

        SerializedProperty keyValuePairs = property.FindPropertyRelative(propname);

        if (!keyValuePairs.isExpanded) {
            return totalheight; 
        }

        totalheight += lineHeight*3; 

        for (int i = 0; i < keyValuePairs.arraySize; i++)
        {
            SerializedProperty pair = keyValuePairs.GetArrayElementAtIndex(i);
            // totalheight += pair.isExpanded ? lineHeight * 3 : lineHeight; 
            totalheight += EditorGUI.GetPropertyHeight(pair);
        }
        

        return totalheight;
    }


}



#endif

}



