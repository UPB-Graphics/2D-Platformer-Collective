using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    public int maxAmmo;
    public int currentAmmo;

    public void Start()
    {
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;
    }
    public void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetAmmo(int ammo)
    {
        currentAmmo = Mathf.Min(maxAmmo, currentAmmo + ammo);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;        
    }
}
