using UnityEngine;

namespace Laserbean.Items
{
    // [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public abstract class ItemData : ScriptableObject, IItem
    {
        public Sprite itemSprite;
        public abstract void OnPickup(IItemCollector itemCollector);
    }
}