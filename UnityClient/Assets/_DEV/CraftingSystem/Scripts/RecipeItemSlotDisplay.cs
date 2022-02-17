using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PlatformerCollective.InventorySystem.Inventory;

namespace PlatformerCollective.CraftingSystem
{
    public class RecipeItemSlotDisplay : MonoBehaviour, IPointerClickHandler
    {
        public readonly UnityEvent<RecipeSlot> LeftClickEvent = new UnityEvent<RecipeSlot>();
        public readonly UnityEvent<RecipeSlot> RightClickEvent = new UnityEvent<RecipeSlot>();

        public Image icon;
        public TMP_Text quantityProgress;
        private RecipeSlot recipeSlot;

        public RecipeSlot RecipeSlot
        {
            set
            {
                if (recipeSlot == null)
                {
                    recipeSlot = value;
                    recipeSlot.RecipeSlotUpdateEvent.AddListener(OnRecipeSlotUpdate);
                    OnRecipeSlotUpdate();
                }
            }
            get { return recipeSlot; }
        }



        private void OnRecipeSlotUpdate()
        {
            if (recipeSlot.item == null)
            {
                icon.gameObject.SetActive(false);
            }
            else
            {
                icon.gameObject.SetActive(true);
                icon.preserveAspect = true;
                icon.sprite = recipeSlot.item.ItemSprite;
            }

            quantityProgress.text = "" + recipeSlot.Amount.ToString() + "/" + recipeSlot.requiredAmount.ToString();
            quantityProgress.gameObject.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                LeftClickEvent.Invoke(recipeSlot);
            }
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                RightClickEvent.Invoke(recipeSlot);
            }
        }
    }
}