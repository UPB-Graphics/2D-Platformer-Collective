using System;
using UnityEngine;

namespace Platformer.Utils
{
    [CreateAssetMenu(menuName = "Platformer/Systems/Event system/SimpleEvent", fileName = "")]
    public class SimpleEvent : ScriptableObject
    {
        private Action _action;

        public void Invoke()
        {
            _action?.Invoke();
        }

        public void Subscribe(Action action)
        {
            _action += action;
        }

        public void Unsubscribe(Action action)
        {
            _action -= action;;
        }
        
    }
}