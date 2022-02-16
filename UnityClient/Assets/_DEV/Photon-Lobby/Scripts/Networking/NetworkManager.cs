using Photon.Pun;
using UnityEngine;

namespace Platformer.Networking
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform[] _spawnPoints;
    
        private void Start()
        {
            if (!PhotonNetwork.OfflineMode)
            { 
                PhotonNetwork.Instantiate(_playerPrefab.name,_spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber].position, Quaternion.identity);
            }
        }
    }
}