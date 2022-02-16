using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Platformer.UI
{
    public class PlayerListing : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _readyText;

        public Photon.Realtime.Player Player { get; private set; }
        private bool _ready;

        public bool Ready
        {
            get => _ready;
            set
            {
                _ready = value;
                _readyText.text = _ready ? "Ready" : "Not ready";
                SetPlayerText(Player);
            }
        }

        public void SetPlayerInfo(Photon.Realtime.Player player)
        {
            Player = player;    
            SetPlayerText(player);
        }

        //public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        //{
        //    base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        //    if (targetPlayer != null && targetPlayer == Player)
        //    {
        //        if (changedProps.ContainsKey("RandomNumber"))
        //        {
        //            SetPlayerText(targetPlayer);
        //        }
        //    }
        //}


        private void SetPlayerText(Photon.Realtime.Player player)
        {
            //int randomNumber = -1;
            //if (player.CustomProperties.ContainsKey("RandomNumber"))
            //{
            //    randomNumber = (int)player.CustomProperties["RandomNumber"];
            //}
            if(!PhotonNetwork.IsMasterClient)
                _text.text = player.NickName + " " + Ready;
            else
                _text.text = player.NickName;
        }
    
    }
}