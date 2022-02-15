using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPlayerController : MonoBehaviour
{
    bool facingRight=true;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if((facingRight && Input.GetKeyDown(KeyCode.A)) || (!facingRight && Input.GetKeyDown(KeyCode.D)))
        {
            Flip();
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        GetComponent<Rigidbody2D>().velocity= new Vector2(5 * inputX, 5 * inputY);

    }

    void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0, 180, 0);

    }
}
