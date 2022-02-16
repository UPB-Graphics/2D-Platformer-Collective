using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class can be used to display on screen text regarding every quest.
/// </summary>
public class QuestLogText : MonoBehaviour
{
	private Text text;

	/// <summary>
	/// Event handler when a quest manager has updates regarding the quests status.
	/// </summary>
	/// <param name="questManager">The quest manager whose event is being handled.</param>
	public void OnQuestUpdate(QuestManager questManager)
	{
		StringBuilder sb = new StringBuilder();
		foreach (Quest quest in questManager.ActiveQuests)
		{
			sb.AppendLine("Active Quest: " + quest.Name);
			sb.AppendLine(questManager.GetQuestProgressText(quest));
		}

		foreach (Quest quest in questManager.QuestsThatAwaitReward)
			sb.AppendLine("Quest " + quest.Name + " is completed and awaits reward");

		foreach (Quest quest in questManager.CompletedQuests)
			sb.AppendLine("Quest " + quest.Name + " is completed");
		text.text = sb.ToString();
	}

	void Awake()
	{
		text = GetComponent<Text>();
	}
}
