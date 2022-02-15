using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flames : MonoBehaviour
{
    [SerializeField] float timer = 3;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        var col = collider.GetComponent<CharacterStats>();
        if (col)
        {
            col.TakeDamage(1);
            yield return new WaitForSeconds(1f);
        }
    }
}
