using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PlatformerCollective.InventorySystem.Inventory
{
    public class ItemSlotDisplay : MonoBehaviour, IPointerClickHandler
    {
        public readonly UnityEvent<ItemSlot> LeftClickEvent = new UnityEvent<ItemSlot>();
        
        private static readonly string NULL_VALIDATION_ERROR = "ItemSlotDisplay has field {0} null!";

        [Header("Components")]
        [SerializeField] 
        private Image itemImage;
        [SerializeField] 
        private TMP_Text quantityTextField;

        private ItemSlot itemSlot;

        public ItemSlot ItemSlot
        {
            set
            {
                if (itemSlot == null)
                {
                    itemSlot = value;
                    itemSlot.ItemSlotUpdateEvent.AddListener(OnItemSlotUpdate);
                    OnItemSlotUpdate();
                }
                else
                {
                    Debug.Log("Cannot assign ItemSlot a second time to the display");
                }

            }
        }
        
        private void OnValidate()
        {
            if (itemImage == null)
            {
                Debug.LogError(string.Format(NULL_VALIDATION_ERROR, "itemImage"));
            }
            
            if (quantityTextField == null)
            {
                Debug.LogError(string.Format(NULL_VALIDATION_ERROR, "quantityTextField"));
            }
        }

        private void OnItemSlotUpdate()
        {
            if (itemSlot.Item == null)
            {
                itemImage.gameObject.SetActive(false);
            }
            else
            {
                itemImage.gameObject.SetActive(true);
                itemImage.preserveAspect = true;
                itemImage.sprite = itemSlot.Item.ItemSprite;
            }

            quantityTextField.text = itemSlot.Quantity.ToString();
            quantityTextField.gameObject.SetActive(itemSlot.Quantity != 0);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                LeftClickEvent.Invoke(itemSlot);
            }
        }
    }
}