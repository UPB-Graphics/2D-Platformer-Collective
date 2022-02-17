using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField]
    float _rotationSpeed = 5f;
    public string PickedUpKey => $"Money-{gameObject.name}-PickedUp";
    public MoneyData MoneyData { get; private set; } = new MoneyData();

    void Start()
    {
        MoneyData.Name = gameObject.name;
    }

    void Update()
    {
        // spin money
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);

        MoneyData.Y = transform.rotation.eulerAngles.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            player.AddMoney();
            gameObject.SetActive(false);
            MoneyData.WasPickedUp = true;
        }
    }

    // ########################################

    public void Load(MoneyData moneyData)
    {
        MoneyData = moneyData;
        gameObject.SetActive(!MoneyData.WasPickedUp);
        transform.rotation = Quaternion.Euler(0, MoneyData.Y, 0);
    }

}
