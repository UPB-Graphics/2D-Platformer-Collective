using Photon.Pun;
using Platformer.Utils;
using UnityEngine;

namespace Platformer.Networking
{
    [CreateAssetMenu(menuName = "Platformer/Game Settings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private SimpleEvent _awake;
        [SerializeField] private bool _offline;
        [SerializeField] private string _gameVersion = "0.0.0";
        private int _randomSalt;

        private void OnEnable()
        {
            _awake.Subscribe(OnAwake);
        }

        private void OnDisable()
        {
            _awake.Unsubscribe(OnAwake);
        }

        public string GameVersion
        {
            get
            {
                return _gameVersion;
            }
        }

        [SerializeField] private string _nickname = "User";

        public string NickName
        {
            get
            {
                if(_randomSalt == 0) _randomSalt = Random.Range(0, 999);
                return _nickname + _randomSalt;
            }
        }

        public void OnAwake()
        {
            PhotonNetwork.OfflineMode = _offline;
        }
    }
}