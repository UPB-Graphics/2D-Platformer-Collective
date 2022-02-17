using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerCollective.InventorySystem.Inventory
{
    public class InventoryInteractor : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 150)]
        private int inventorySlotCount = 1;

        [SerializeField]
        private List<Item> initialItems;

        public InventoryHolder InventoryHolder
        {
            private set;
            get;
        }

        private void Awake()
        {
            InventoryHolder = new InventoryHolder(inventorySlotCount);
            initialItems.ForEach(item => InventoryHolder.Add(item, 1));
        }
    }
}