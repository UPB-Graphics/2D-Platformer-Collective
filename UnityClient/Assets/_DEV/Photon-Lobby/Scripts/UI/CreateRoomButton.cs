using Photon.Pun;
using Photon.Realtime;
using Platformer.Networking;
using UnityEngine;

namespace Platformer.UI
{
    public class CreateRoomButton : ButtonElement
    {
        [SerializeField] protected RoomProfile _roomProfile;
        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            CreateRoom();
        }

        protected virtual void CreateRoom()
        {
            if (!PhotonNetwork.IsConnected) return;
            if (PhotonNetwork.NetworkClientState == ClientState.Joining) return;
            if (PhotonNetwork.NetworkClientState == ClientState.ConnectingToGameServer) return;
            if (PhotonNetwork.NetworkClientState == ClientState.ConnectingToMasterServer) return;
            if (PhotonNetwork.NetworkClientState == ClientState.Authenticating) return;
            var options = _roomProfile.GetRoomOptions();
            PhotonNetwork.JoinOrCreateRoom(MasterManager.GameSettings.NickName, options, TypedLobby.Default);
        }
    }
}