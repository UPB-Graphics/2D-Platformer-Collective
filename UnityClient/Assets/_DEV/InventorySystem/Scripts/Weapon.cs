using UnityEngine;

namespace PlatformerCollective.InventorySystem
{
    public class Weapon : EquippableItem
    {
        [SerializeField]
        private float baseRange;
        [SerializeField]
        private float baseDamage;
    }
}