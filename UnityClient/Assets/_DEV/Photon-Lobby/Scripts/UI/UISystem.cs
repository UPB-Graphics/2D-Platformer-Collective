using System;
using Platformer.Utils;
using Unity.Collections;
using UnityEngine;

namespace Platformer.UI
{
    [CreateAssetMenu(menuName = "Platformer/Systems/UI/UISystem")]
    public class UISystem : ScriptableObject
    {
        [ReadOnly] public UIState CurrentState;

        [HideInInspector] public Action<UIState> onStateChanged;
        
        [ReadOnly] private UIState lockState;
        [ReadOnly] private UIState memorizedState;
        
        public void ChangeState(UIState newState)
        {
            if (memorizedState != null)
            {
                var cacheState = memorizedState;
                memorizedState = null;
                ChangeState(cacheState);
                return;
            }
            
            if (lockState == null || lockState == newState)
            {
                CurrentState = newState;
                onStateChanged.Fire(CurrentState);

            }
        }
        
        public void Reset()
        {
            CurrentState = null;
            memorizedState = null;
        }
    }
}
