using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject flames;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Character" && collision.gameObject.tag != "Fire")
        {
            var position = transform.position;
            var rotation = transform.rotation;
            GetComponent<Animator>().SetTrigger("Stop");
            StartCoroutine(Spawn(position, rotation));
        }

    }

    IEnumerator Spawn(Vector3 position, Quaternion rotation)
    {
        Instantiate(flames, position + new Vector3(0, 0.5f, 0), rotation);
        for (int i = 1; i < 3; i++)
        {
            yield return new WaitForSeconds(0.15f);
            Instantiate(flames, position + new Vector3(0.6f * i, 0.5f, 0), rotation);
            Instantiate(flames, position + new Vector3(-0.6f * i, 0.5f, 0), rotation);
        }
        Destroy(gameObject);
    }
}
