using UnityEngine;

namespace Platformer.UI
{
    public class UIStateButton : ButtonElement
    {
        [SerializeField] private UIState _state;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            _state.Activate();
        }
    }
}