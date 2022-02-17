using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_v2 : MonoBehaviour
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
        // save
        var json = JsonUtility.ToJson(transform.position); // returns a string in JSON format
        PlayerPrefs.SetString("PlayerPosition", json);
    }

    private void OnEnable()
    {
        // load
        if (PlayerPrefs.HasKey("PlayerPosition"))
        {
            // load playerprefs into json
            var json = PlayerPrefs.GetString("PlayerPosition");
            // make a vector3 from json and set transform.position = to it
            transform.position = JsonUtility.FromJson<Vector3>(json);
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
