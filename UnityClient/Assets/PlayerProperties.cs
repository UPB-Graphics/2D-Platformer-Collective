using System;

using UnityEngine;

[Serializable]
public class PlayerProperties : ISerializationCallbackReceiver
{
#region gameplay
	private const int energyFull = 2; // 10;

	[SerializeField]
	private int energy = energyFull;
	// public int Energy() => energy;

	[SerializeField]
	private int lifePoints = 2; // 10
	// public int LifePoints() => lifePoints;

	public bool decreaseEnergy() => (--energy) <= 0;
	public bool decreaseLifePoints()
	{
		energy = energyFull;
		return (--lifePoints) <= 0;
	}
#endregion

#region serialization_version
	private Transform transform;

	[SerializeField]
	private Vector3 position;
	[SerializeField]
	private Quaternion rotation;

	public void Save(PlayerController p)
	{
		transform = p.transform;

		Persistance.Save(
			"PlayerController",
			JsonUtility.ToJson(this)
		);
	}
	public void Load(PlayerController c)
	{
		string json = Persistance.Load("PlayerController");
		if (json == null) return;

		PlayerProperties p = JsonUtility.FromJson<PlayerProperties>(json);

		c.transform.position = p.position;
		c.transform.rotation = p.rotation;
		this.energy = p.energy;
		this.lifePoints = p.lifePoints;	
	}

	public void OnAfterDeserialize() {}
	public void OnBeforeSerialize()
	{
		if (transform == null) return;

		position = transform.position;
		rotation = transform.rotation;
	}
#endregion
}
