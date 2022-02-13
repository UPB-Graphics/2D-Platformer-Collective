using UnityEngine;
using UnityEngine.UI;

//test class to test the other functionalities and demo
public class TestUI : MonoBehaviour
{
	public GameObject testUI; //test window rerference
	void Start()
	{
		testUI.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			testUI.SetActive(!testUI.activeSelf);
		}
	}

	//add 30 HP
	public void AddHealth()
    {
		PlayerStatsManager.instance.SetHealth(30);
	}

	//remove 30 HP
	public void SubstractHealth()
	{
		PlayerStatsManager.instance.SetHealth(-30);
	}

	//add 10 gold
	public void AddGold()
	{
		PlayerStatsManager.instance.SetGold(10);
	}

	//add 50 exp
	public void AddExp50()
	{
		PlayerStatsManager.instance.SetExp(50);
	}
	//add 100 exp
	public void AddExp100()
	{
		PlayerStatsManager.instance.SetExp(100);
	}
	//add 500 exp
	public void AddExp500()
	{
		PlayerStatsManager.instance.SetExp(500);
	}

	//equip sword
	public void AddSword()
    {
		if(PlayerStatsManager.instance.SpendMoney(35) == true)
			PlayerStatsManager.instance.AddModifiersForEquipment(20,0);

	}

	//equip shield
	public void AddShield()
	{
		if (PlayerStatsManager.instance.SpendMoney(35) == true)
			PlayerStatsManager.instance.AddModifiersForEquipment(0, 15);

	}

	//unequip al, items
	public void UnequipAll()
	{
		PlayerStatsManager.instance.RemoveAllModifiers();

	}

	//close the test window
	public void CloseWindow()
    {
		testUI.SetActive(false);
	}
}