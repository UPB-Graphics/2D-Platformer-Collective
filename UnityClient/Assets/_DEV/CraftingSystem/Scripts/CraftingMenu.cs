using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerCollective.InventorySystem.Inventory;

namespace PlatformerCollective.CraftingSystem
{
    public class CraftingMenu : MonoBehaviour
    {
        public CraftingManager craftingManager;
        public RectTransform recipeContainer;
        public GameObject recipeObject;
        public GameObject recipeArrow;
        public RecipeItemSlotDisplay recipeItemSlotDisplayPrefab;
        public InventoryInteractor inventoryInteractor;

        private List<List<RecipeSlot>> craftingArray;

        void Start()
        {
            craftingArray = new List<List<RecipeSlot>>();
            recipeContainer.sizeDelta = new Vector2(recipeContainer.sizeDelta.x, 135 * (craftingManager.recipes.Length + 1));
            foreach (CraftingRecipe craftingRecipe in craftingManager.recipes)
            {
                List<RecipeSlot> craftingGroup = new List<RecipeSlot>();
                GameObject uiRecipeSlot = Instantiate(recipeObject, recipeContainer);
                foreach (RecipeSlot recipeSlot in craftingRecipe.recipeContent)
                {
                    RecipeItemSlotDisplay recipeItemSlotDisplay = Instantiate(recipeItemSlotDisplayPrefab, uiRecipeSlot.transform);
                    recipeItemSlotDisplay.RecipeSlot = new RecipeSlot(recipeSlot.item, recipeSlot.requiredAmount);
                    craftingGroup.Add(recipeItemSlotDisplay.RecipeSlot);
                    recipeItemSlotDisplay.LeftClickEvent.AddListener(LeftClickOnRecipeSlotCallback);
                    recipeItemSlotDisplay.RightClickEvent.AddListener(RightClickOnRecipeSlotCallback);
                }
                Instantiate(recipeArrow, uiRecipeSlot.transform);
                RecipeItemSlotDisplay recipeProduct = Instantiate(recipeItemSlotDisplayPrefab, uiRecipeSlot.transform);
                recipeProduct.RecipeSlot = new RecipeSlot(craftingRecipe.recipeProduct.item, craftingRecipe.recipeProduct.requiredAmount);
                craftingGroup.Insert(0, recipeProduct.RecipeSlot);
                recipeProduct.LeftClickEvent.AddListener(LeftClickOnRecipeProductCallback);
                craftingArray.Add(craftingGroup);
            }
            gameObject.SetActive(false);
        }

        private void CheckIfRecipeIsComplete(RecipeSlot recipeSlot)
        {
            if (recipeSlot.Amount == recipeSlot.requiredAmount)
            {
                List<RecipeSlot> craftingGroup = null;
                foreach (List<RecipeSlot> crGr in craftingArray)
                {
                    if (crGr.Contains(recipeSlot))
                    {
                        craftingGroup = crGr;
                        break;
                    }
                }
                if (craftingGroup != null)
                {
                    bool isCompleted = true;
                    for (int i = 1; i < craftingGroup.Count && isCompleted; ++i)
                    {
                        if (craftingGroup[i].Amount < craftingGroup[i].requiredAmount) isCompleted = false;
                    }
                    if (isCompleted)
                    {
                        craftingGroup[0].SetAmount(craftingGroup[0].requiredAmount);
                    }
                    else
                    {
                        craftingGroup[0].SetAmount(0);
                    }
                }
            }
            else
            {
                List<RecipeSlot> craftingGroup = null;
                foreach (List<RecipeSlot> crGr in craftingArray)
                {
                    if (crGr.Contains(recipeSlot))
                    {
                        craftingGroup = crGr;
                        break;
                    }
                }
                if (craftingGroup != null)
                {
                    craftingGroup[0].SetAmount(0);
                }
            }
        }

        private ItemSlot CanAddToRecipe(RecipeSlot recipeSlot)
        {
            IReadOnlyList<ItemSlot> itemSlots = inventoryInteractor.InventoryHolder.ItemSlots;
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.HasItem(recipeSlot.item) && !itemSlot.IsSlotEmpty())
                {
                    return itemSlot;
                }
            }
            return null;
        }

        private void LeftClickOnRecipeSlotCallback(RecipeSlot recipeSlot)
        {
            ItemSlot itemInInventory = CanAddToRecipe(recipeSlot);
            if (itemInInventory == null) return;
            if (recipeSlot.Amount != recipeSlot.IncreaseAmount())
            {
                itemInInventory.Remove(1);
                CheckIfRecipeIsComplete(recipeSlot);
            }
        }

        private void RightClickOnRecipeSlotCallback(RecipeSlot recipeSlot)
        {
            if (recipeSlot.Amount != recipeSlot.DecreaseAmount())
            {
                inventoryInteractor.InventoryHolder.Add(recipeSlot.item, 1);
                CheckIfRecipeIsComplete(recipeSlot);
            }
        }

        private void ResetRecipe(RecipeSlot recipeSlot)
        {
            List<RecipeSlot> craftingGroup = null;
            foreach (List<RecipeSlot> crGr in craftingArray)
            {
                if (crGr.Contains(recipeSlot))
                {
                    craftingGroup = crGr;
                    break;
                }
            }
            if (craftingGroup != null)
            {
                foreach (RecipeSlot slot in craftingGroup)
                {
                    slot.SetAmount(0);
                }
            }
        }

        private void LeftClickOnRecipeProductCallback(RecipeSlot recipeSlot)
        {
            inventoryInteractor.InventoryHolder.Add(recipeSlot.item, recipeSlot.Amount);
            ResetRecipe(recipeSlot);
        }

        public void ClearCraftZone()
        {
            foreach (List<RecipeSlot> craftingGroup in craftingArray)
            {
                for (int i = 1; i < craftingGroup.Count; ++i)
                {
                    if (craftingGroup[i].Amount > 0)
                    {
                        inventoryInteractor.InventoryHolder.Add(craftingGroup[i].item, craftingGroup[i].Amount);
                        craftingGroup[i].SetAmount(0);
                    }
                }
                craftingGroup[0].SetAmount(0);
            }
        }
    }
}