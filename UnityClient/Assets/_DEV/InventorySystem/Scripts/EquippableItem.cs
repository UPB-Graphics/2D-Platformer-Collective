using UnityEngine.Events;

namespace PlatformerCollective.InventorySystem
{
    public abstract class EquippableItem : Item
    {
        public readonly UnityEvent<EquippableItem> EquipEvent = new UnityEvent<EquippableItem>();
        public readonly UnityEvent<EquippableItem> UnequipEvent = new UnityEvent<EquippableItem>();
        
        
        public virtual void Equip()
        {
            EquipEvent.Invoke(this);
        }

        public virtual void Unequip()
        {
            UnequipEvent.Invoke(this);
        }
    }
}