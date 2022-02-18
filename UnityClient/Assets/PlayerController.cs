using UnityEngine;

/// VERY basic player controller/logic
public class PlayerController : MonoBehaviour
{
	private PlayerProperties properties = new PlayerProperties();

	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool groundedPlayer;

	public bool twoPointFiveD = true; /// 2 & 2.5D

	private const float playerSpeed = 10.0f;
	private const float jumpHeight = 3.0f;
	private const float gravityValue = -9.81f * 3;
	private const float offScreenY = -10.0f;

	private void CheckLoadSave()
	{
		/// prevent load/save if not grounded
		if (!groundedPlayer) return;

		if (Input.GetButtonDown("Save"))
			properties.Save(this);

		else if (Input.GetButtonDown("Load"))
			properties.Load(this);
	}

	private void UseEnergy()
	{
		if (properties.decreaseEnergy())
			if (properties.decreaseLifePoints())
				Lose();
	}
	private void Lose()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
	private bool IsOffScreen()
	{
		if (gameObject.transform.position.y < offScreenY)
			return true;

		// todo? add check on X axis

		return false;
	}

	void Update()
	{
		groundedPlayer = controller.isGrounded;

		if (groundedPlayer && playerVelocity.y < 0)
			playerVelocity.y = 0f;

		float z = 0.0f;
		if (!twoPointFiveD) /// 3D
			z = Input.GetAxis("Vertical");

		Vector3 move = new Vector3(
			Input.GetAxis("Horizontal"), 0, z
		);

		controller.Move(move * Time.deltaTime * playerSpeed);

		if (move != Vector3.zero)
			gameObject.transform.forward = move;

		if (Input.GetButtonDown("Jump") && groundedPlayer)
		{
			playerVelocity.y += Mathf.Sqrt(
				jumpHeight * -3.0f * gravityValue
			);

			UseEnergy();
		}

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);

		/// VERY basic loss condition
		if (IsOffScreen())
			Lose();

		CheckLoadSave();
	}

	private void Start()
	{
		controller =
			gameObject.GetComponent<CharacterController>() ??
			gameObject.AddComponent<CharacterController>()
		;
	}
}
