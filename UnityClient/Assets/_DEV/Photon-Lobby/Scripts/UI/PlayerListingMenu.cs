using System.Collections.Generic;
using Photon.Pun;
using Platformer.Utils;
using UnityEngine;

namespace Platformer.UI
{
    public class PlayerListingMenu : MonoBehaviourPunCallbacks, UIBehaviour_Component
    {
        [SerializeField] private UIState _createOrJoinState;
        [SerializeField] private Transform _content;
        [SerializeField] private PlayerListing _playerListing;
        [SerializeField] private BoolVariable _ready;
        
        private List<PlayerListing> _listings = new List<PlayerListing>();
       
        
        public void Activated()
        {
            SetReadyUp(false);
            GetCurrentRoomPlayer();
        }

        public void Deactivated()
        {
            foreach (var t in _listings)
            {
                Destroy(t.gameObject);
            }

            _listings.Clear();
        }

        private void SetReadyUp(bool state)
        {
            _ready.Value = state;
        }

        private void GetCurrentRoomPlayer()
        {
            if (!PhotonNetwork.IsConnected)
                return;
            if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
                return;
            foreach (KeyValuePair<int, Photon.Realtime.Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
            {
                AddPlayerListing(playerInfo.Value);
            }
        }

        private void AddPlayerListing(Photon.Realtime.Player player)
        {
            int index = _listings.FindIndex(x => x.Player == player);
            if (index != -1)
            {
                _listings[index].SetPlayerInfo(player);
            }
            else
            {
                PlayerListing listing = Instantiate(_playerListing, _content);
                if (listing != null)
                {
                    listing.SetPlayerInfo(player);
                    _listings.Add(listing);
                }
            }
        }

        public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
        { 
            PhotonNetwork.LeaveRoom(true);
            _createOrJoinState.Activate();
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            AddPlayerListing(newPlayer);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            int index = _listings.FindIndex(x => x.Player == otherPlayer);
            if (index != -1)
            {
                Destroy(_listings[index].gameObject);
                _listings.RemoveAt(index);
            }
        }

        public void OnClick_StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                for (var i = 0; i < _listings.Count; i++)
                {
                    if (_listings[i].Player != PhotonNetwork.LocalPlayer)
                    {
                        if (!_listings[i].Ready)
                            return;
                    }
                }
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.LoadLevel(1);
            }
        }

        public void OnClick_ReadyUp()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                SetReadyUp(!_ready.Value);
                base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer, _ready.Value);
            }
        }
        
        [PunRPC]
        private void RPC_ChangeReadyState(Photon.Realtime.Player player, bool ready)
        {
            int index = _listings.FindIndex(x => x.Player == player);
            if (index != -1)
            {
                _listings[index].Ready = ready;
            }
        }
    }
}