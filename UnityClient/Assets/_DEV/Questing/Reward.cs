using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A reward is given to the player when it completes a quest.
/// </summary>
public abstract class Reward : ScriptableObject
{
	/// <summary>
	/// This method is called to give the rewards to the player.
	/// </summary>
	/// <param name="character"><see cref="GameObject"/> that is the player. </param>
	public abstract void GiveReward(GameObject character);
}
