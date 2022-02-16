using System;
using Photon.Pun;
using Platformer.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI
{
    public class PrivateRoomId : MonoBehaviour, UIBehaviour_Component
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private StringVariable _roomId;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }
        
        public void Activated()
        {
            _image.enabled = !PhotonNetwork.CurrentRoom.IsVisible;
            _text.text = PhotonNetwork.CurrentRoom.IsVisible ? "" : $"Room id: {_roomId.Value}";
        }

        public void Deactivated() { }
    }

}