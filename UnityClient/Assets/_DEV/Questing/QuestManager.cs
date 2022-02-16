using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;

/// <summary>
/// This component tracks quests for the player. It contains three lists: the active quests,
/// the completed quests but await reward and the completed quests.
/// </summary>
public class QuestManager : MonoBehaviour
{
	/// <summary>
	/// Quests that are in progress.
	/// </summary>
	public List<Quest> ActiveQuests;

	/// <summary>
	/// Quests that are completed but await reward.
	/// </summary>
	public List<Quest> QuestsThatAwaitReward;

	/// <summary>
	/// Quests that are completed.
	/// </summary>
	public List<Quest> CompletedQuests;

	/// <summary>
	/// Private dictionary used to track the progress for each quest.
	/// </summary>
	private Dictionary<Quest, List<Tuple<Goal, GoalProgress>>> QuestProgress;

	/// <summary>
	/// Event that is raised when there is an update regarding the quests status.
	/// </summary>
	public UnityEvent<QuestManager> QuestsUpdate;


	/// <summary>
	/// Checks if the manager has the quest in any of his lists.
	/// </summary>
	/// <param name="quest">The quest in question.</param>
	/// <returns></returns>
	public bool HasQuest(Quest quest)
	{
		return ActiveQuests.Contains(quest) || QuestsThatAwaitReward.Contains(quest) || CompletedQuests.Contains(quest);
	}

	/// <summary>
	/// Adds a new quest for the player. The quest is added to <see cref="ActiveQuests"/>.
	/// If the quest is already in any of the manager's list, this function does nothing.
	/// </summary>
	/// <param name="quest">The quest to add.</param>
	public void AddQuest(Quest quest)
	{
		if (HasQuest(quest))
			return;
		ActiveQuests.Add(quest);
		List<Tuple<Goal, GoalProgress>> goalProgresses = new List<Tuple<Goal, GoalProgress>>();
		foreach (Goal goal in quest.Goals)
			goalProgresses.Add(new Tuple<Goal, GoalProgress>(goal, goal.StartProgress()));
		QuestProgress.Add(quest, goalProgresses);
		QuestsUpdate?.Invoke(this);
	}

	/// <summary>
	/// This method can be used to return a text to display on screen about the progress of a quest.
	/// </summary>
	/// <param name="quest">The quest in question.</param>
	/// <returns>A text to display on screen about the progress of a quest.</returns>
	public string GetQuestProgressText(Quest quest)
	{
		if (QuestsThatAwaitReward.Contains(quest))
			return "Quest is completed and awaits reward.";
		if (CompletedQuests.Contains(quest))
			return "Quest is completed.";

		if (QuestProgress.TryGetValue(quest, out List<Tuple<Goal, GoalProgress>> goalProgresses))
			return string.Join("\n", goalProgresses.Select(tuple => tuple.Item2.ToString()));
		return string.Empty;
	}

	/// <summary>
	/// This method is called when the player should recieve the rewards for a quest. The method checks if the quest awaits rewards.
	/// </summary>
	/// <param name="quest">The quest in question.</param>
	public void GetRewards(Quest quest)
	{
		if (!QuestsThatAwaitReward.Remove(quest))
			return;
		foreach (Reward reward in quest.Rewards)
			reward.GiveReward(gameObject);
		CompletedQuests.Add(quest);
		QuestsUpdate?.Invoke(this);
	}

	/// <summary>
	/// This method is called when the player should recieve all rewards from all the quests that await reward.
	/// </summary>
	public void GetAllRewards()
	{
		List<Quest> quests = new List<Quest>(QuestsThatAwaitReward);
		quests.ForEach((quest) => GetRewards(quest));
	}

	/// <summary>
	/// When a player completes a certain action, a <see cref="Deed"/> is created and passed to this class. The manager then updates all quest progresses.
	/// </summary>
	/// <param name="deed"></param>
	public void ProcessDeed(Deed deed)
	{
		foreach (var kvp in QuestProgress)
		{
			kvp.Value.ForEach(tuple => tuple.Item1.UpdateProgress(tuple.Item2, deed));
			if (kvp.Value.All(tuple => tuple.Item2.IsCompleted))
				QuestsThatAwaitReward.Add(kvp.Key);
		}
		IEnumerable<Quest> ToRemoveQuests = ActiveQuests.Where(quest => QuestsThatAwaitReward.Contains(quest));
		foreach (Quest quest in ToRemoveQuests)
			QuestProgress.Remove(quest);
		ActiveQuests.RemoveAll(quest => QuestsThatAwaitReward.Contains(quest));
		QuestsUpdate?.Invoke(this);
	}

    // Start is called before the first frame update
    void Start()
    {
		QuestProgress = new Dictionary<Quest, List<Tuple<Goal, GoalProgress>>>(ActiveQuests.Count);
		List<Quest> activeQuests = new List<Quest>(ActiveQuests);
		ActiveQuests.Clear();
		foreach (Quest quest in activeQuests)
			AddQuest(quest);
		QuestsUpdate?.Invoke(this);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
