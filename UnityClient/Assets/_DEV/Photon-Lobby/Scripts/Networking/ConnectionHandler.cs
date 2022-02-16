using Photon.Pun;
using Photon.Realtime;
using Platformer.UI;
using UnityEngine;

namespace Platformer.Networking
{
    public class ConnectionHandler : MonoBehaviourPunCallbacks
    {
        [SerializeField] private UIState _homepage;
        
        private void Start()
        {
            PhotonNetwork.GameVersion = "0.0.1";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
            PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            Debug.Log("Connected to server");
            Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        
            _homepage.Activate();
            if(!PhotonNetwork.InLobby)
                PhotonNetwork.JoinLobby(TypedLobby.Default);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            Debug.Log("Disconnected" + cause);
        }
    }
}