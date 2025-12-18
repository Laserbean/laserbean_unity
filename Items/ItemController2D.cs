using Laserbean.Removable;
using UnityEngine;


namespace Laserbean.Items
{

    [RequireComponent(typeof(RemoveAfter))]
    public class ItemController2D : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer; 
        RemoveAfter disableAfter;
        public ItemData itemData;

        void Start()
        {
            disableAfter = GetComponent<RemoveAfter>();
            spriteRenderer.sprite = itemData.itemSprite; 
        }
        
        void OnTriggerEnter2D(Collider2D collision)
        {
            var itemecollector = collision.gameObject.GetComponent<IItemCollector>();
            if (itemecollector != null)
            {
                itemecollector.PickupItem(itemData);
                disableAfter.StartRemoveAfter();
            }
        }
    }
}
