using UnityEngine;

namespace Laserbean.Items
{
    public class ItemCollector : MonoBehaviour, IItemCollector
    {
        public void PickupItem(ItemData itemData)
        {
            itemData.OnPickup(this);
        }
    }
}
