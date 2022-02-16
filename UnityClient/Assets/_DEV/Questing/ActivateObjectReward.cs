using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This reward activates some objects in scene.
/// </summary>
[CreateAssetMenu(fileName = "New Activate Object Reward", menuName = "Questing/Rewards/Activate Object Reward", order = 1)]
public class ActivateObjectReward : Reward
{
	/// <summary>
	/// An identifier used to specify the typee of objects to activate.
	/// </summary>
	public string Name;

	public override void GiveReward(GameObject character)
	{
		foreach (RewardActivator activator in FindObjectsOfType<RewardActivator>(true))
			if (activator.Name == Name)
				activator.Activate();
	}
}
