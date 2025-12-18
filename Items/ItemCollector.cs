using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Laserbean.Items
{
    public class ItemCollector : MonoBehaviour, IItemCollector
    {

        List<IItemCollector> itemCollectors = new();

        void Start()
        {
            itemCollectors = gameObject.GetComponentsInChildren<IItemCollector>().ToList();
            itemCollectors.Remove(this); 
        }

        public void PickupItem(ItemData itemData)
        {
            foreach (var comp in itemCollectors)
            {
                comp.PickupItem(itemData); 
            }
        }
    }
}
