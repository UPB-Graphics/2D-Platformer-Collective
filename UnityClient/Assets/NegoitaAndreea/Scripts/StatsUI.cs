using UnityEngine;
using UnityEngine.UI;

//class that represents the stats UI for the player
public class StatsUI : MonoBehaviour
{
	public GameObject statsUI; //reference to the stats window
	public Slider healthSlider; //reference to the health bar
	public Slider expSlider; //reference to the experience bar

	public Text attack;  //reference to the attack text field, from the stats window
	public Text defense;  //reference to the defence text field, from the stats window
	public Text health;  //reference to the health text field, from the stats window
	public Text exp;  //reference to the exp text field, from the stats window
	public Text expNextlevel;  //reference to the exp until the next level text field, from the stats window
	public Text gold;  //reference to the gold text field, from the stats window
	public Text level;  //reference to the level text field from the whole screen

	void Start()
	{
		//initialize the UI
		UpdateUI();
		//then deactivate the window
		statsUI.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.L)) //when pressing the L key, enable the stats window
		{
			statsUI.SetActive(!statsUI.activeSelf);
			UpdateUI();
		}
	}

	//populate the text fields from the stats window
	void UpdateUI()
	{
		attack.text = PlayerStatsManager.instance.GetDamage().ToString();
		defense.text = PlayerStatsManager.instance.GetArmor().ToString();
		health.text = PlayerStatsManager.instance.GetHealth().ToString();
		exp.text = PlayerStatsManager.instance.GetExp().ToString();
		gold.text = PlayerStatsManager.instance.GetGold().ToString();
		healthSlider.value = PlayerStatsManager.instance.GetHealth();
		expSlider.value = PlayerStatsManager.instance.GetExpFormatted();
		level.text = (PlayerStatsManager.instance.GetLevel() + 1).ToString();
		expNextlevel.text = (PlayerStatsManager.instance.GetNextExp() - PlayerStatsManager.instance.GetExp()).ToString();
	}

	public void UpdateHealthText()
	{
		health.text = PlayerStatsManager.instance.GetHealth().ToString(); //update the text field
		healthSlider.value = PlayerStatsManager.instance.GetHealth(); //update the health bar
	}

	public void UpdateAttack()
	{
		attack.text = PlayerStatsManager.instance.GetDamage().ToString();
	}

	public void UpdateDefense()
	{
		defense.text = PlayerStatsManager.instance.GetArmor().ToString();
	}

	public void UpdateExpText()
	{
		exp.text = PlayerStatsManager.instance.GetExp().ToString();  //update the exp text field
		expSlider.value = PlayerStatsManager.instance.GetExpFormatted(); //update the exp bar
		level.text = (PlayerStatsManager.instance.GetLevel() + 1).ToString(); //update the levels text field
		expNextlevel.text = (PlayerStatsManager.instance.GetNextExp() - PlayerStatsManager.instance.GetExp()).ToString(); //update the exp until next level text field
	}

	public void UpdateGoldText()
	{
		gold.text = PlayerStatsManager.instance.GetGold().ToString();
	}

	public void CloseWindow()
    {
		statsUI.SetActive(false);
	}

	//when the level inscreases, then inscrease the UI to accomodate the new maximum health value
	public void SetHealthBarSize(float value)
    {
		float oldScale = healthSlider.transform.localScale.x;
		healthSlider.transform.localScale = new Vector3(oldScale + healthSlider.maxValue / (value*15) , 1, 1);
		healthSlider.maxValue = value;
		
	}
}