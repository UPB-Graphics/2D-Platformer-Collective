using TMPro;
using UnityEngine;

namespace Platformer.UI
{
    public class PrivateRoomInputButton : ButtonElement
    {
        [SerializeField] private TextMeshProUGUI _input;
        private TextMeshProUGUI _thisText;
    
        private void Awake()
        {
            _thisText = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            _input.text += _thisText.text;
        }
    }

}