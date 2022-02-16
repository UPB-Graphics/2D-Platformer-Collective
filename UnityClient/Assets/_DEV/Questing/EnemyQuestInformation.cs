using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component can be used to give a <see cref="DestroyDeed"/> when this component is destroyed.
/// </summary>
public class EnemyQuestInformation : MonoBehaviour
{
	/// <summary>
	/// An identifier used to specify the type of the enemy that was destroyed.
	/// </summary>
	public string Name;

	private QuestManager questManager;

	private void Start()
	{
		questManager = FindObjectOfType<QuestManager>();
	}

	private void OnDestroy()
	{
		questManager.ProcessDeed(new DestroyDeed(Name));
	}
}
