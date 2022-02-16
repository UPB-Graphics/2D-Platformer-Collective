namespace Platformer.UI
{
    public class PrivateRoomInput : TextElement, UIBehaviour_Component
    {
        public void Activated()
        {
            ThisText.text = "";
        }

        public void Deactivated() { }
    }
}