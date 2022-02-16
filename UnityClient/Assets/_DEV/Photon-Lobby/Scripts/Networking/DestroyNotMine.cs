using Photon.Pun;
using UnityEngine;

namespace Platformer.Networking
{
    public class DestroyNotMine : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _gameObject;
        private void Awake()
        {
            if (!photonView.IsMine)
            {
                Destroy(_gameObject);
            }
        }
    }
}