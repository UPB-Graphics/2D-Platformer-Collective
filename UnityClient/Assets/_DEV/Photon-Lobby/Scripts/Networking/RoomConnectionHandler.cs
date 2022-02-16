using Photon.Pun;
using Platformer.UI;
using UnityEngine;

namespace Platformer.Networking
{
    public class RoomConnectionHandler : MonoBehaviourPunCallbacks
    {
        [SerializeField] private UIState _currentRoomState;
    
    
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            _currentRoomState.Activate();
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Created room successfully!");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Room creation failed!");
        }
    }
}