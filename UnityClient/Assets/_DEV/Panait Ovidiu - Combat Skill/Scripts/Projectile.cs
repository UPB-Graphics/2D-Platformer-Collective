using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private string parentName;

    private float startX;
    private bool isLeft;

    private bool isStatic = false;

    private float speed = 10f;
    private float range = 10f;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        if(transform.localScale.x == -1)
        {
            isLeft = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeft)
        {
            if (isStatic)
            {

                transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, 0);
                if (startX - transform.position.x > range)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (isStatic)
            {
                transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, 0);
                if (startX - transform.position.x < -range)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetRange(float range)
    {
        this.range = range;
    }

    public void SetParent(string parent)
    {
        parentName = parent;
    }

    public string GetParent()
    {
        return parentName;
    }

    public void SetStatic()
    {
        isStatic = true;
    }

    public void SetDirection(bool isLeft)
    {
        this.isLeft = isLeft;
    }
}
