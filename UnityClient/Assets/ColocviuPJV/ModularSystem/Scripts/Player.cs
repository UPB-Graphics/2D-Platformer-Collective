using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    float _speed = 3f;

    public int Money { get; set; }

    Rigidbody _rigidbody;

    private void Awake()
    {
        // cache the rigid body component 
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // read input 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // set rigid body velocity 
        Vector3 velocity = new Vector3(horizontal, 0, vertical).normalized;
        // inertia  
        if (velocity.magnitude > 0)
        {
            _rigidbody.velocity = velocity * _speed;
        }
    }


    // ########################################
    public void AddMoney()
    {
        Money++;
    }

    // // ########################################
    // // implement ISaveState methods
    // public void Save(int gameNumber)
    // {
    //     // serialize to json using builtin class JsonUtility
    //     var json = JsonUtility.ToJson(transform.position); // returns a string in JSON format
    //     PlayerPrefs.SetString(gameNumber + "PlayerPosition", json);
    //     // save velocity as well
    //     var velocityJson = JsonUtility.ToJson(_rigidbody.velocity);
    //     PlayerPrefs.SetString(gameNumber + "PlayerVelocity", velocityJson);

    //     // save money 
    //     PlayerPrefs.SetInt(gameNumber + "PlayerMoney", Money);
    // }

    // public void Load(int gameNumber)
    // {
    //     if (PlayerPrefs.HasKey(gameNumber + "PlayerPosition"))
    //     {
    //         // load playerprefs into json
    //         var json = PlayerPrefs.GetString(gameNumber + "PlayerPosition");
    //         // make a vector3 from json and set transform.position = to it
    //         transform.position = JsonUtility.FromJson<Vector3>(json);

    //         // load velocity
    //         var velocityJson = PlayerPrefs.GetString(gameNumber + "PlayerVelocity");
    //         _rigidbody.velocity = JsonUtility.FromJson<Vector3>(velocityJson);

    //         // load money score
    //         Money = PlayerPrefs.GetInt(gameNumber + "PlayerMoney");
    //     }
    // }

}

