using System;
using UnityEngine;

namespace Platformer.Utils
{
    public class ReactiveVariable<T> : ScriptableObject
    {
        public Action onValueChanged;

        [SerializeField, HideInInspector] protected T val;

        public virtual T Value
        {
            get => val;
            set
            {
                val = value;
                onValueChanged?.Invoke();
            }
        }

        public virtual void SetValEventless(T newValue)
        {
            val = newValue;
        }
    }
}