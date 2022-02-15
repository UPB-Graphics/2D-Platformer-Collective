using System;
using UnityEngine;

namespace PlatformerCollective.InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "InventorySystem/Item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField]
        private string itemName;
        [SerializeField]
        private Sprite itemSprite;
        [SerializeField]
        [Range(0, 1000)]
        private int stackSize;
        [SerializeField]
        private int value;

        public string ItemName => itemName;
        public Sprite ItemSprite => itemSprite;
        public int StackSize => stackSize;
        public int Value => value;
        
        private void OnValidate()
        {
            if (itemSprite == null)
            {
                Debug.LogError("Item sprite is missing on item!");
            }
        }
    }    
}
