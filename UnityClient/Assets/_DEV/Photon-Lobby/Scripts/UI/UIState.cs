using UnityEngine;

namespace Platformer.UI
{
    [CreateAssetMenu(menuName = "Platformer/Systems/UI/UIState")]
    public class UIState : ScriptableObject
    {
        [SerializeField] private UISystem _system;

        public void Activate()
        {
            _system.ChangeState(this); 
        }
    }
}