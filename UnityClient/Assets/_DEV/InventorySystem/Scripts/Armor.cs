using UnityEngine;

namespace PlatformerCollective.InventorySystem
{
    [CreateAssetMenu(fileName = "New Armor Piece", menuName = "InventorySystem/Armor", order = 2)]
    public class Armor : EquippableItem
    {
        [SerializeField]
        private ArmorType armorType;
        [SerializeField]
        private float maximumHealthModifier;
        [SerializeField]
        private float healthRegenModifier;
        [SerializeField]
        private float maximumManaModifier;
        [SerializeField]
        private float manaRegenModifier;
        [SerializeField]
        private float resistanceModifier;

        public float MaximumHealthModifier => maximumHealthModifier;

        public float HealthRegenModifier => healthRegenModifier;

        public float MaximumManaModifier => maximumManaModifier;

        public float ManaRegenModifier => manaRegenModifier;

        public float ResistanceModifier => resistanceModifier;

        public ArmorType ArmorType => armorType;
    }
}