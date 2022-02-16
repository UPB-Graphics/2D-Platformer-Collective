using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWave : MonoBehaviour
{
    private string parentName;

    private float startX;
    private bool isLeft;

    public float hitDamage = 10f;
    public float attackRateSec = 0.75f;

    private bool isStatic = false;
    private float projectileLifeSec = 1.75f;
    private float projectileStartTime = 0.0f;

    void Start()
    {
        startX = transform.position.x;
        projectileStartTime = Time.time + projectileLifeSec;
        if(transform.localScale.x == -1)
        {
            isLeft = true;
        }
    }

    void Update()
    {
        if (isLeft)
        {
            if (isStatic)
            {
                if (Time.time > projectileStartTime)
                {
                    projectileStartTime = Time.time + projectileLifeSec;
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x - (10f * Time.deltaTime), transform.position.y, 0);
                if (startX - transform.position.x > 10f) //verifica deplasarea proiectilului(stanga) / >10 == delete
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (isStatic)
            {
                if (Time.time > projectileStartTime)
                {
                    projectileStartTime = Time.time + projectileLifeSec;
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x + (10f * Time.deltaTime), transform.position.y, 0);
                if (startX - transform.position.x < -10f)  //verifica deplasarea proiectilului(dreapta) / >10 == delete
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) //la coliziune, proiectilul se distruge
        {
            Destroy(gameObject);
        }
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
