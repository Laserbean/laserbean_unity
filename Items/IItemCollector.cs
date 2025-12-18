using UnityEngine;

namespace Laserbean.Items
{
    public interface IItemCollector
    {
        public void PickupItem(ItemData itemData);
    }


    public interface IItem
    {
        public void OnPickup(IItemCollector itemCollector);
    }
}