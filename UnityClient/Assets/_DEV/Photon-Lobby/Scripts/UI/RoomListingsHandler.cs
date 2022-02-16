using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Platformer.Utils;
using UnityEngine;

namespace Platformer.UI
{
    public class RoomListingsHandler : MonoBehaviourPunCallbacks
    {

        [SerializeField] private Transform _content;
        [SerializeField] private RoomListing _roomListing;
    
        private List<RoomListing> _listings = new List<RoomListing>();

        public override void OnJoinedRoom()
        {
            _content.DestroyChildren();
            _listings.Clear();
        }
    

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach (RoomInfo info in roomList)
            {
                if (info.RemovedFromList)
                {
                    int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (index != -1)
                    {
                        Destroy(_listings[index].gameObject);
                        _listings.RemoveAt(index);
                    }
                }
                else
                {
                    var index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                    if (index == -1)
                    {
                        RoomListing listing = Instantiate(_roomListing, _content);
                        if (listing != null)
                        {
                            listing.SetRoomInfo(info);
                            _listings.Add(listing);
                        }
                    }
                }
            }
        }
    }
}