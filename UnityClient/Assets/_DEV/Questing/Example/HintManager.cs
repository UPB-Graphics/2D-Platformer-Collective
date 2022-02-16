using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Component to show the player how to solve the quests.
/// </summary>
public class HintManager : MonoBehaviour
{
	private Text text;

	private readonly string questsStillNotTaken = "Get quests.";
	private readonly string ballQuestString = "Click on the ball to destroy it.";
	private readonly string boxesQuestString = "Click on the boxes to destroy them.";
	private readonly string rewardString = "Get the rewards.";
	private readonly string doneString = "You are done.";

	public Quest BallQuest;
	public Quest GreenBoxesQuest;

	void Awake()
    {
		text = GetComponent<Text>();
	}

	/// <summary>
	/// Event handler when a quest manager has updates regarding the quests status.
	/// </summary>
	/// <param name="questManager">The quest manager whose event is being handled.</param>
	public void OnQuestUpdate(QuestManager questManager)
	{
		if (questManager.ActiveQuests.Contains(BallQuest))
			text.text = ballQuestString;
		else if (questManager.QuestsThatAwaitReward.Contains(BallQuest))
			text.text = rewardString;
		else if (questManager.ActiveQuests.Contains(GreenBoxesQuest))
			text.text = boxesQuestString;
		else if (questManager.QuestsThatAwaitReward.Contains(GreenBoxesQuest))
			text.text = rewardString;
		else if (!questManager.HasQuest(BallQuest) || !questManager.HasQuest(GreenBoxesQuest))
			text.text = questsStillNotTaken;
		else text.text = doneString;
	}
}
