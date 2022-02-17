using UnityEngine;

public class Interactable : MonoBehaviour
{
	public float radius = 3f;
	float aux;

	bool isFocus = false;
	Transform player;

	bool hasInteracted = false;

	public Transform interactionTransform;


	void Start()
	{
		aux = radius;
	}

	public virtual void Interact()
	{

	}

	void Update()
	{
		var obj = GameObject.FindGameObjectsWithTag("Bow");
		if (obj[0].GetComponent<Renderer>().enabled == true)
		{
			radius = 8f;
		}

		else
		{
			radius = 3f;
		}

		if (isFocus && !hasInteracted)
		{
			float distance = Vector3.Distance(player.position, interactionTransform.position);
			if (distance <= radius)
			{
				Interact();
				hasInteracted = true;
			}
		}
	}

	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}

	public void OnDefocused()
	{
		isFocus = false;
		player = null;
		hasInteracted = false;
	}

	void OnDrawGizmosSelected()
	{
		if (interactionTransform == null)
			interactionTransform = transform;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}

}