using Photon.Pun;

namespace Platformer.UI
{
    public class LeaveRoomButton : ButtonElement
    {
        protected override void OnButtonClick()
        {
            Click();
        }

        public void Click()
        {
            PhotonNetwork.LeaveRoom(true);
        }
    }
}