using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that keeps track of all the stats of a character
public class Stats : MonoBehaviour
{
	//health and gold are not a Stat class as they are not behaving in the same way as attack or armor
	public int maxHealth = 100;
	public int currentHealth { get; private set; }
	public int gold { get; private set; }

	public Levels levelSystem; //level system to keep track of the experience and levels

	public Stat damage;
	public Stat armor;

	void Awake()
	{
		//initialize health and the level system
		currentHealth = maxHealth;
		levelSystem = new Levels();
	}

	//used mostly when healing
	public void ModifyHealth(int value)
	{
		currentHealth += value;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}

	//used mostly when taking damage
	public void TakeDamage(int damage)
	{
		damage -= armor.GetValue(); //first take into consideration the armor value
		damage = Mathf.Clamp(damage, 0, int.MaxValue);
		currentHealth -= damage;  //then substract the remaining damage

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Die();
		}
	}

	public void ModifyMoney(int value)
	{
		gold += value;
		if (gold < 0)
			gold = 0;
	}

	//exp can only be added
	public void ModifyExp(int value)
	{
		levelSystem.AddExp(value);
	}

	//for each item equiped, add the corresponding modifiers
	public void AddModifiersCorespondingToEquipment(int weaponDamage, int armorDefense)
    {
		armor.AddValue(armorDefense);
		damage.AddValue(weaponDamage);
	}
	//if an item is unequiped, then remove its modifiers
	public void RemoveModifiersCorespondingToEquipment(int weaponDamage, int armorDefense)
	{
		armor.RemoveValue(armorDefense);
		damage.RemoveValue(weaponDamage);
	}

	public void RemoveAttackModifiers()
    {
		damage.RemoveAll();
	}

	public void RemoveArmorModifiers()
	{
		armor.RemoveAll();
	}

	public virtual void Die()
	{
		Debug.Log(transform.name + " died");
		StartCoroutine(DieAnimation());
	}

	private IEnumerator DieAnimation()
	{
		//Do code to activate death animation
		yield return new WaitForSeconds(2f);
		//Destroy(gameObject);
	}

	//when a character levels up, then the maximum health is increased and is healed up
	public void IncreaseMaxHealth()
    {
		maxHealth += levelSystem.GetLevel() * 2;
		currentHealth = maxHealth;
	}

}