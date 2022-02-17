using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float parallexEffect;


    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float distance = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if( temp< startPos - length) startPos -= length;
    }
}
