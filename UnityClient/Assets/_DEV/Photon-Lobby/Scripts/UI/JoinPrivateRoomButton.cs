using Photon.Pun;
using Platformer.Utils;
using TMPro;
using UnityEngine;

namespace Platformer.UI
{
    public class JoinPrivateRoomButton : ButtonElement
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private StringVariable _roomId;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            PhotonNetwork.JoinRoom(_name.text);
            _roomId.Value = _name.text;
        }
    }
}