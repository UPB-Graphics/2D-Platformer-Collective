using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This goal is used to create quest where the player needs to destroy enemies.
/// </summary>
[CreateAssetMenu(fileName = "New Destroy Goal", menuName = "Questing/Goals/Destroy Goal", order = 1)]
public class DestroyGoal : Goal
{
	/// <summary>
	/// An identifier used to specify a type of enemies to destroy.
	/// </summary>
	public string EnemyName;

	/// <summary>
	/// How many enemies to destroy.
	/// </summary>
	public int Amount;

	/// <summary>
	/// Private class used to track progress of the enemies destroyed by the player.
	/// </summary>
	private class DestroyProgress : GoalProgress
	{
		public DestroyGoal Goal;
		public int Progress;
		public override bool IsCompleted => Progress >= Goal.Amount;

		public DestroyProgress(DestroyGoal goal)
		{
			Goal = goal;
			Progress = 0;
		}

		public override string ToString()
		{
			return "Destroy " + Goal.EnemyName + " " + Progress + "/" + Goal.Amount;
		}
	}

	public override GoalProgress StartProgress()
	{
		return new DestroyProgress(this);
	}

	public override void UpdateProgress(GoalProgress goalProgress, Deed deed)
	{
		DestroyDeed destroyDeed = deed as DestroyDeed;
		if (destroyDeed != null && destroyDeed.EnemyName == EnemyName)
			++(goalProgress as DestroyProgress).Progress;
	}
}

/// <summary>
/// Use this deed to inform <see cref="QuestManager"/> when an enemy has been destroyed.
/// </summary>
public class DestroyDeed : Deed
{
	/// <summary>
	/// An identifier used to specify the type of the enemy that was destroyed.
	/// </summary>
	public readonly string EnemyName;

	/// <summary>
	/// Constructs a new deed that an enemy was destroyed. Pass this new instance to <see cref="QuestManager"/>.
	/// </summary>
	/// <param name="enemyName">An identifier used to specify the type of the enemy that was destroyed.</param>
	public DestroyDeed(string enemyName)
	{
		EnemyName = enemyName;
	}
}
