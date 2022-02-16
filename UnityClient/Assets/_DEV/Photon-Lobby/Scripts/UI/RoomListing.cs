using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Platformer.UI
{
    public class RoomListing : ButtonElement
    {
        [SerializeField] private TextMeshProUGUI _text;
    
        public RoomInfo RoomInfo { get; private set; }
        public void SetRoomInfo(RoomInfo roomInfo)
        {
            RoomInfo = roomInfo;
            _text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
        }

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            PhotonNetwork.JoinRoom(RoomInfo.Name);
        }
    }

}