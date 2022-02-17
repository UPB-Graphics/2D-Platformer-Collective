using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformerCollective.InventorySystem.Inventory
{
    public class ItemSlot
    {
        public readonly UnityEvent ItemSlotUpdateEvent = new UnityEvent();

        public Item Item
        {
            get;
            private set;
        }

        public int Quantity
        {
            get;
            private set;
        }

        public ItemSlot()
        {
            Item = null;
            Quantity = 0;
        }

        public ItemSlot(Item _item)
        {
            Item = _item;
            Quantity = 0;
        }
        
        /// <summary>
        /// Adds a quantity of a given item to the inventory slot.
        /// If the item slot has a different item or is already full,
        /// based on the item type, no transaction will be made.
        /// </summary>
        ///
        /// <param name="item">Item to add to the slot</param>
        /// <param name="quantity">Quantity to be added of given item</param>
        /// 
        /// <returns>
        /// Quantity of items that was added to the item slot
        /// </returns>
        public int Add(Item item, int quantity)
        {
            if (item == null ||
                !(Item == null || Item.Equals(item)))
                return 0;

            Item = item;

            int maximumAmountThatCouldBeAdded = Mathf.Max(0, Item.StackSize - Quantity);

            if (quantity > maximumAmountThatCouldBeAdded)
            {
                Quantity += maximumAmountThatCouldBeAdded;
                ItemSlotUpdateEvent.Invoke();
                return maximumAmountThatCouldBeAdded;
            }

            Quantity += quantity;
            ItemSlotUpdateEvent.Invoke();
            return quantity;
        }

        public int Add(Item item) => Add(Item, 1);

        /// <summary>
        /// Removes a quantity from the item slot.
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns>Quantity of item that was removed</returns>
        public int Remove(int quantity)
        {
            int amountRemoved = 0;
            
            if (Item == null)
            {
                return amountRemoved;
            }

            if (quantity >= Quantity)
            {
                amountRemoved = Quantity;
            }
            else
            {
                amountRemoved = quantity;
            }

            Quantity -= amountRemoved;

            if (Quantity == 0)
                Item = null;
            
            ItemSlotUpdateEvent.Invoke();
            return amountRemoved;
        }

        /// <summary>
        /// Moves the item from this item slot to another
        /// </summary>
        /// <param name="itemSlot">Receiving item slot</param>
        /// <returns>Whether or not the operation was a success</returns>
        public bool TransferTo(ItemSlot itemSlot)
        {
            int amountAdded = itemSlot.Add(Item, Quantity);
            if (amountAdded == 0)
                return false;

            Remove(amountAdded);
            return true;
        }

        /// <summary>
        /// Verifies if the item slot contains a specific item
        /// </summary>
        /// <param name="item">Item to be checked</param>
        /// <returns>Whether or not given item is contained</returns>
        public bool HasItem(Item item)
        {
            if (Item == null)
                return false;
            return Item.Equals(item);
        }

        /// <summary>
        /// Verifies if slot is full
        /// </summary>
        public bool IsSlotFull()
        {
            return Item != null && Quantity == Item.StackSize;
        }

        /// <summary>
        /// Verifies if slot is empty
        /// </summary>
        public bool IsSlotEmpty()
        {
            return Item == null || Quantity == 0;
        }
        
    }
}