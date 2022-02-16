using Photon.Pun;
using Platformer.UI;

namespace Platformer.Networking
{
    public class HideUIOnMaster : MonoBehaviourPunCallbacks, UIBehaviour_Component
    {
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            if(PhotonNetwork.IsMasterClient) gameObject.SetActive(false);
        }

        public void Activated()
        {
            if(PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient) gameObject.SetActive(false);
        }

        public void Deactivated() { }
    }

}