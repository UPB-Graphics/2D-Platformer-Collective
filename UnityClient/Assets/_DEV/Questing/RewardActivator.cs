using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Component that activates the <see cref="GameObject"/> when <see cref="ActivateObjectReward"/> is given.
/// </summary>
public class RewardActivator : MonoBehaviour
{
	/// <summary>
	/// An identifier used to specify the type of the object that is activated.
	/// </summary>
	public string Name;

	/// <summary>
	/// Activate the object.
	/// </summary>
	public void Activate()
	{
		gameObject.SetActive(true);
	}
}
