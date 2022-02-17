using UnityEngine;
using UnityEngine.UI;

public class UIMoneyDisplay : MonoBehaviour
{
    private Player _player;
    private Text _text;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _text = GetComponent<Text>();
    }


    void Update()
    {
        _text.text = $"${_player.Money}";
    }
}
