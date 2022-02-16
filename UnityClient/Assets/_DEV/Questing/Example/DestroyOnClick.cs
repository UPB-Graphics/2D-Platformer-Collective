using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to test <see cref="DestroyDeed"/>. Destroys the object when you click on it.
/// </summary>
public class DestroyOnClick : MonoBehaviour
{
	private void OnMouseDown()
	{
		Destroy(gameObject);
	}
}
