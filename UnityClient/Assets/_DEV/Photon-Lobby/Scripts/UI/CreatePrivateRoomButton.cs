using Photon.Pun;
using Photon.Realtime;
using Platformer.UI;
using Platformer.Utils;
using UnityEngine;

public class CreatePrivateRoomButton : CreateRoomButton
{
    [SerializeField] private StringVariable _privateRoomId;
    
    protected override void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;
        if (PhotonNetwork.NetworkClientState == ClientState.Joining) return;
        if (PhotonNetwork.NetworkClientState == ClientState.ConnectingToGameServer) return;
        if (PhotonNetwork.NetworkClientState == ClientState.ConnectingToMasterServer) return;
        if (PhotonNetwork.NetworkClientState == ClientState.Authenticating) return;
        var options = _roomProfile.GetRoomOptions();
        var name = Random.Range(1000, 9999).ToString();
        _privateRoomId.Value = name;
        PhotonNetwork.JoinOrCreateRoom(name, options, TypedLobby.Default);
    }
}
