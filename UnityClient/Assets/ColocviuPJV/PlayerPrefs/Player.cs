using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float _speed = 3f;
    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        // save position using PlayerPrefs // key-value 
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            // retrieve vals; can have second param to GetFloat - for a default value
            var x = PlayerPrefs.GetFloat("PlayerX");
            var y = PlayerPrefs.GetFloat("PlayerY");
            transform.position = new Vector3(x, y, 0);
        }
    }

    void Update()
    {
        // movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 velocity = new Vector2(horizontal, vertical).normalized;
        // Vector3 velocity = new Vector3(horizontal, 0, vertical).normalized;
        _rigidbody.velocity = velocity * _speed;

    }
}
