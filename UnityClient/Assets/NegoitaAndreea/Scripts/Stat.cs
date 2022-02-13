using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
	[SerializeField]
	private int baseValue; //base value of the stat
	private List<int> modifiers = new List<int>(); //all the modifiers cummulated from weapons, armor, potions etc

	//return the value of the stat, including the modifiers
	public int GetValue() 
	{
		int value = baseValue;
		modifiers.ForEach(x => value += x);
		return value;
	}

	//add a modifier to the list
	public void AddValue(int value)
	{
		if (value != 0)
			modifiers.Add(value);
	}

	//remove a modifier from the list
	public void RemoveValue(int value)
	{
		if (value != 0)
			modifiers.Remove(value);
	}

	//remove all modifiers, for example when the player unequips all items
	public void RemoveAll()
    {
		modifiers = new List<int>();
	}

}