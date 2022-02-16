using Photon.Pun;

namespace Platformer.UI
{
    public class HideUIOnClient : MonoBehaviourPunCallbacks, UIBehaviour_Component
    {
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            if(!PhotonNetwork.IsMasterClient) gameObject.SetActive(false);
        }

        public void Activated()
        {
            if(PhotonNetwork.IsConnected && !PhotonNetwork.IsMasterClient) gameObject.SetActive(false);
        }

        public void Deactivated() { }
    }
}