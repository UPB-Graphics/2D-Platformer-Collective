using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a quest. A quest is a series of goals after which you get results.
/// </summary>
[CreateAssetMenu(fileName = "New Quest", menuName = "Questing/Quest", order = 1)]
public class Quest : ScriptableObject
{
	/// <summary>
	/// Name of the quest.
	/// </summary>
	public string Name;
	/// <summary>
	/// Quest description.
	/// </summary>
	public string Description;

	/// <summary>
	/// List of goals.
	/// </summary>
	public List<Goal> Goals;
	/// <summary>
	/// List of Rewards.
	/// </summary>
	public List<Reward> Rewards;
}
