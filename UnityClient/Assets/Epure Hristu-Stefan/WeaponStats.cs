using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public int attackPower;
    public float attackRange;
    public float attackRate;
    public int enchantLevel;

    private void Awake()
    {
        enchantLevel = 0;
    }

    public void SetEnchantLevel()
    {
        if(enchantLevel >= 5)
        {
            Debug.Log("This weapon is at the maximum enchant level!");
            return;
        }

        enchantLevel += 1;
        attackPower = (int)Mathf.Ceil(attackPower * 1.2f);
        attackRange *= 1.1f;
        attackRate *= 1.2f;
    }

    public void SetAttackPower(int newAttackPower)
    {
        attackPower = newAttackPower;
    }

    public void SetAttackRange(float newAttackRange)
    {
        attackRange = newAttackRange;
    }

    public void SetAttackRate(float newAttackRate)
    {
        attackRate = newAttackRate;
    }
}
