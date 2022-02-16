using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonElement : MonoBehaviour
{
    private Button _button;
    public Button ThisButton => _button ? _button : (_button = GetComponentInChildren<Button>(true));

    protected virtual void OnEnable()
    {
        SubscribeToClick();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeToClick();
    }

    protected virtual void OnButtonClick() { }
        
    private void SubscribeToClick()
    {
        ThisButton.onClick.AddListener(OnButtonClick);
    }

    private void UnsubscribeToClick()
    {
        ThisButton.onClick.RemoveListener(OnButtonClick);
    }
}