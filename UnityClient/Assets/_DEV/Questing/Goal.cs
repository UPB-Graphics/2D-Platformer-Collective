using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A goal is a part of a quest. Goals can be reused for different quests.
/// This class is a <see cref="ScriptableObject"/> therefore it it shouldn't be changed during runtime.
/// </summary>
public abstract class Goal : ScriptableObject
{
	/// <summary>
	/// When a player starts a quest, this method returns a <see cref="GoalProgress"/> that is used to track progress of the quest.
	/// Therefore this class remains unchanged during runtime.
	/// </summary>
	/// <returns></returns>
	public abstract GoalProgress StartProgress();
	/// <summary>
	/// This method is used by <see cref="QuestManager"/> to update the progress after the player completes a deed.
	/// </summary>
	/// <param name="goalProgress">Current progress. Use the value returned by <see cref="StartProgress"/>.</param>
	/// <param name="deed">The deed completed by a player.</param>
	public abstract void UpdateProgress(GoalProgress goalProgress, Deed deed);
}

/// <summary>
/// <para>This class is used to track progress of a <see cref="Goal"/>.</para>
/// <para><see cref="Object.ToString"/> can be used to get a string that can be displayed on screen to show the goal progress.</para>
/// </summary>
public abstract class GoalProgress
{
	/// <summary>
	/// Returns if the goal has been completed.
	/// </summary>
	public abstract bool IsCompleted { get; }
}

/// <summary>
/// A deed is something the player does that is used to change progress of a quest.
/// </summary>
/// <example><see cref="DestroyDeed"/></example>
public abstract class Deed
{
}