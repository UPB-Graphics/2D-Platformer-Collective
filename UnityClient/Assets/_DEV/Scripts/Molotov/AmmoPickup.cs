using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int amount=3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<CharacterStats>().GetAmmo(amount);
        Destroy(gameObject);
    }
}
