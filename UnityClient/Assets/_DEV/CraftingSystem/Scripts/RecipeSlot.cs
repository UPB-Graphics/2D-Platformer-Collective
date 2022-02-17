using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PlatformerCollective.InventorySystem;

namespace PlatformerCollective.CraftingSystem
{
    [System.Serializable]
    public class RecipeSlot
    {
        public readonly UnityEvent RecipeSlotUpdateEvent = new UnityEvent();
        public Item item;
        private int amount;
        public int requiredAmount;

        public RecipeSlot(Item _item, int _amount)
        {
            item = _item;
            amount = 0;
            requiredAmount = _amount;
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public int IncreaseAmount()
        {
            if (amount < requiredAmount) amount++;
            RecipeSlotUpdateEvent.Invoke();
            return amount;
        }

        public int DecreaseAmount()
        {
            if (amount > 0) amount--;
            RecipeSlotUpdateEvent.Invoke();
            return amount;
        }

        public void SetAmount(int _amount)
        {
            amount = Mathf.Clamp(_amount, 0, requiredAmount);
            RecipeSlotUpdateEvent.Invoke();
        }
    }
}
