using Photon.Realtime;
using UnityEngine;

namespace Platformer.Networking
{
    [CreateAssetMenu(menuName = "Platformer/Networking/Room profile")]
    public class RoomProfile : ScriptableObject
    {
        [SerializeField] private byte _maxPlayers;
        [SerializeField] private bool _isVisible;

        private RoomOptions _roomOptions;

        private void OnEnable()
        {
            _roomOptions =  new RoomOptions
            {
                MaxPlayers = _maxPlayers, BroadcastPropsChangeToAll = true, IsVisible = _isVisible
            };
        }

        public RoomOptions GetRoomOptions()
        {
            return _roomOptions;
        }
    }
}