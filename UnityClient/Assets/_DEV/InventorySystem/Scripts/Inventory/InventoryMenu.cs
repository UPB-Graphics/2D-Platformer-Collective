using UnityEngine;

namespace PlatformerCollective.InventorySystem.Inventory
{
    public class InventoryMenu : MonoBehaviour
    {
        private static readonly string NULL_VALIDATION_ERROR = "InventoryMenu has field {0} null!";

        [SerializeField]
        private Transform parentForItemSlotDisplays;

        [SerializeField]
        private InventoryInteractor playerInventoryInteractor;

        [SerializeField]
        private ItemSlotDisplay itemSlotDisplayPrefab;

        [SerializeField]
        private ItemPickup itemPickupPrefab;

        private ItemSlot currentlySelectedItemSlot;
        
        private void OnValidate()
        {
            if (parentForItemSlotDisplays == null)
            {
                Debug.LogError(string.Format(NULL_VALIDATION_ERROR, "parentForItemSlotDisplays"));
            }

            if (itemSlotDisplayPrefab == null)
            {
                Debug.LogError(string.Format(NULL_VALIDATION_ERROR, "itemSlotDisplayPrefab"));
            }

            if (itemPickupPrefab == null)
            {
                Debug.LogError(string.Format(NULL_VALIDATION_ERROR, "itemPickupPrefab"));
            }
        }
        
        private void Awake()
        {
            foreach (var itemSlot in playerInventoryInteractor.InventoryHolder.ItemSlots)
            {
                ItemSlotDisplay itemSlotDisplay = Instantiate(itemSlotDisplayPrefab, parentForItemSlotDisplays);
                itemSlotDisplay.ItemSlot = itemSlot;
                itemSlotDisplay.LeftClickEvent.AddListener(LeftClickOnItemSlotCallback);
            }
        }
        
        private void LeftClickOnItemSlotCallback(ItemSlot itemSlot)
        {
            if (currentlySelectedItemSlot == null && itemSlot.IsSlotEmpty())
                return;
            
            if (currentlySelectedItemSlot == null)
            {
                currentlySelectedItemSlot = itemSlot;
                return;
            }

            if (currentlySelectedItemSlot == itemSlot)
            {
                ItemPickup itemPickup = Instantiate(itemPickupPrefab, 
                    playerInventoryInteractor.transform.position,
                    Quaternion.identity);

                itemPickup.item = currentlySelectedItemSlot.Item;
                itemPickup.quantity = currentlySelectedItemSlot.Quantity;
                currentlySelectedItemSlot.Remove(currentlySelectedItemSlot.Quantity);
                currentlySelectedItemSlot = null;
                return;
            }
            
            currentlySelectedItemSlot.TransferTo(itemSlot);
            currentlySelectedItemSlot = null;
        }
    }
}