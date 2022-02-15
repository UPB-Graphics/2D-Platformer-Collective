using UnityEngine;
using UnityEngine.Events;

namespace PlatformerCollective.InventorySystem
{
    public abstract class Consumable : Item
    {
        public readonly UnityEvent<Consumable> ConsumeEvent = new UnityEvent<Consumable>();

        protected abstract void OnConsume();
    }
}