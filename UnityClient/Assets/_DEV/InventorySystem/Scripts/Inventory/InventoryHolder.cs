using System.Collections.Generic;

namespace PlatformerCollective.InventorySystem.Inventory
{
    /// <summary>
    /// Class that holds the item slots of a given container-type object. Most
    /// of the operations with the item slots could be applied directly to
    /// the slots themselves, given they do hold logic. This is a gateway
    /// for more general operations.
    /// </summary>
    public class InventoryHolder
    {
        private readonly List<ItemSlot> _itemSlots;
        public IReadOnlyList<ItemSlot> ItemSlots => _itemSlots;

        public InventoryHolder(int size)
        {
            _itemSlots = new List<ItemSlot>();
            for (int i = 0; i < size; i++)
            {
                _itemSlots.Add(new ItemSlot());
            }
        }

        /// <summary>
        /// Adds a quantity of a given item to the inventory. The inventory
        /// holder searches for an item slot that has the same item to add
        /// said quantity to it, then tries empty slots.
        /// Use this method for interactions in the world space (similar to pick-ups)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        /// <returns>Quantity of item that was added successfully</returns>
        public int Add(Item item, int quantity)
        {
            int amountAdded = 0;
            
            // Try to find an item slot that already has that same item
            foreach (var itemSlot in _itemSlots)
            {
                if (itemSlot.HasItem(item) && !itemSlot.IsSlotFull())
                {
                    int amount = itemSlot.Add(item, quantity);
                    amountAdded += amount;
                    quantity -= amount;
                    if (quantity == 0)
                        break;
                }
            }

            if (quantity == 0)
                return amountAdded;
            
            // Try to find an empty item slot
            foreach (var itemSlot in _itemSlots)
            {
                if (itemSlot.IsSlotEmpty())
                {
                    int amount = itemSlot.Add(item, quantity);
                    amountAdded += amount;
                    quantity -= amount;
                    if (quantity == 0)
                        break;
                }
            }

            return amountAdded;
        }
    }
}