using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class TextElement : MonoBehaviour
{
    private TextMeshProUGUI _text;
    protected TextMeshProUGUI ThisText
        => _text ? _text : _text = GetComponent<TextMeshProUGUI>();
        
    protected virtual void SetText(string text)
    {
        ThisText.text = text;
    }
}