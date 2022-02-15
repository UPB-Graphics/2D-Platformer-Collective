using System;
using UnityEngine;

namespace PlatformerCollective.InventorySystem.Inventory
{
    public class ItemPickup : MonoBehaviour
    {
        [Header("Item information")]
        public Item item;
        public int quantity;

        private SpriteRenderer spriteRenderer;
        
        private bool canBePickedUp;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (item == null)
            {
                Debug.LogError("ItemPickup does not have an item attached!");
                return;
            }

            spriteRenderer.sprite = item.ItemSprite;
            spriteRenderer.size = new Vector2(2.56f, 2.56f);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player") && canBePickedUp)
            {
                var inventoryInteractor = col.GetComponent<InventoryInteractor>();
                int amountAdded = inventoryInteractor.InventoryHolder.Add(item, quantity);
                quantity -= amountAdded;
                if (quantity <= 0)
                    Destroy(gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                canBePickedUp = true;
            }
        }
    }
}