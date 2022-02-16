using TMPro;
using UnityEngine;

namespace Platformer.UI
{
    public class PrivateRoomBackspace : ButtonElement
    {
        [SerializeField] private TextMeshProUGUI _input;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            _input.text = _input.text.Remove(_input.text.Length - 1);
        }
    }

}