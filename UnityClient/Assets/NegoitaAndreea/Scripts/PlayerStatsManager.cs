using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Singleton class that keeps track of the player stats and of the UI. Should be added as a component on the Game Manager Object
public class PlayerStatsManager : MonoBehaviour
{
	#region Singleton

	public static PlayerStatsManager instance;

	void Awake()
	{
		instance = this;
		//subscribe to the level change event
		player.GetComponent<Stats>().levelSystem.OnLevelChanged += LevelsOnLevelChanged;
	}

    #endregion

    public GameObject player; //player reference
	public StatsUI statsUI;  //status window reference

	//In case the player dies, restart the scene
	public void KillPlayer()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//used mostly when gaining gold
	public void SetGold(int gold)
	{
		if (player.GetComponent<Stats>())
		{
			player.GetComponent<Stats>().ModifyMoney(gold);
			statsUI.UpdateGoldText(); //update the UI
		}
	}

	//used mostly when spending gold
	public bool SpendMoney(int gold)
	{
		if (player.GetComponent<Stats>())
		{
			if (DoIHaveEnoughMoney(gold) == true)
			{
				player.GetComponent<Stats>().ModifyMoney(-gold);
				statsUI.UpdateGoldText(); //update the UI
				return true;
			}
		}
		return false;
	}

	//in case of spending money, check if the player has enough gold
	public bool DoIHaveEnoughMoney(int gold)
    {
		if (player.GetComponent<Stats>())
		{
			if (player.GetComponent<Stats>().gold >= gold)
				return true;
			else
				return false;
		}
		return false;
	}

	//gold getter
	public int GetGold()
	{
		if (player.GetComponent<Stats>())
		{
			return player.GetComponent<Stats>().gold;

		}
		else
			return 0;
	}

	//modify health, either by heal or taking damage
	public void SetHealth(int value)
	{
		if (player.GetComponent<Stats>())
		{
			if (value > 0)
				player.GetComponent<Stats>().ModifyHealth(value);
			else
				player.GetComponent<Stats>().TakeDamage(-value);
			statsUI.UpdateHealthText();

		}
	}

	//health getter
	public int GetHealth()
	{
		if (player.GetComponent<Stats>())
		{
			return player.GetComponent<Stats>().currentHealth;

		}
		else
			return 0;
	}

	//exp getter
	public int GetExp()
	{
		if (player.GetComponent<Stats>())
		{
			return player.GetComponent<Stats>().levelSystem.GetExp();

		}
		else
			return 0;
	}

	public float GetExpFormatted()
	{
		if (player.GetComponent<Stats>())
		{
			return player.GetComponent<Stats>().levelSystem.GetExpFormatted();

		}
		else
			return 0;
	}

	//exp until the next level getter
	public int GetNextExp()
	{
		if (player.GetComponent<Stats>())
		{
			return player.GetComponent<Stats>().levelSystem.GetExpNextLevel();

		}
		else
			return 0;
	}

	//level getter
	public int GetLevel()
	{
		if (player.GetComponent<Stats>())
		{
			return player.GetComponent<Stats>().levelSystem.GetLevel();

		}
		else
			return 0;
	}

	//exp stter
	public void SetExp(int value)
	{
		if (player.GetComponent<Stats>())
		{
			player.GetComponent<Stats>().ModifyExp(value);
			statsUI.UpdateExpText(); //update UI

		}
	}

	//damage getter
	public int GetDamage()
	{
		if (player.GetComponent<Stats>())
		{
			return player.GetComponent<Stats>().damage.GetValue();

		}
		else
			return 0;
	}

	//armor getter
	public int GetArmor()
	{
		if (player.GetComponent<Stats>())
		{
			return player.GetComponent<Stats>().armor.GetValue();

		}
		else
			return 0;
	}

	//when an item is equiped, call this function
	public void AddModifiersForEquipment(int dmg, int def)
    {
		player.GetComponent<Stats>().AddModifiersCorespondingToEquipment(dmg, def);
		//update UI
		statsUI.UpdateAttack();
		statsUI.UpdateDefense();
	}

	//when an item is unequiped, call this function
	public void RemoveModifiersForEquipment(int dmg, int def)
	{
		player.GetComponent<Stats>().RemoveModifiersCorespondingToEquipment(dmg, def);
		//update UI
		statsUI.UpdateAttack();
		statsUI.UpdateDefense();
	}

	//when all items are unequiped, call this function
	public void RemoveAllModifiers()
    {
		player.GetComponent<Stats>().RemoveArmorModifiers();
		player.GetComponent<Stats>().RemoveAttackModifiers();
		//update UI
		statsUI.UpdateAttack();
		statsUI.UpdateDefense();
	}

	//function subscribed to the level change event
	private void LevelsOnLevelChanged(object sender, EventArgs e)
	{
		Debug.Log("Do something then the level changes");
		player.GetComponent<Stats>().IncreaseMaxHealth();  //increase the maximum health
		statsUI.SetHealthBarSize(player.GetComponent<Stats>().maxHealth);  //update this change on the health bar
		statsUI.UpdateHealthText(); //update the UI
		
	}
}