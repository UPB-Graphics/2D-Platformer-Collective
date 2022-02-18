using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    float horizontalMove = 0.0f;
    public float runSpeed = 40f;
    bool jump = false;
    public Text youDiedTextReference;

    public Vector3 lastCheckpoint;
    Vector3 initialPosition;
    int livesLeft = 3;
    bool mDeadState = false;

    string youDiedInitialText = "You died! Lives left: ";

    // Start is called before the first frame update
    void Start()
    {
        lastCheckpoint = transform.localPosition;
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("HurtingEnvironment"))
        {
            // set the "dead" state
            mDeadState = true;
            YouDied();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            // save the checkpoints position so we can go back to it if we die
            lastCheckpoint = collision.transform.position;
            collision.gameObject.SetActive(false);
        }
    }

    private async void RestartGameInThree()
    {
        // firstly we show the UI element, letting the user know how many lives he/she has left
        youDiedTextReference.text += livesLeft.ToString();
        youDiedTextReference.gameObject.SetActive(true);
        if (--livesLeft < 0)
        {
            youDiedTextReference.text = "You died to many times :(. Game will restart in 3 seconds.";
            lastCheckpoint = initialPosition;
        }
        await Task.Delay(3000);

        // disable the UI element
        youDiedTextReference.gameObject.SetActive(false);

        // reposition to the last checkpoint and revert the player's color
        this.transform.localPosition = lastCheckpoint;
        this.GetComponent<SpriteRenderer>().color = Color.white;
        mDeadState = false;

        // reset the UI text
        youDiedTextReference.text = youDiedInitialText;
    }

    private void YouDied()
    {
        // change the player's color to emphasize the fact he/she died
        this.GetComponent<SpriteRenderer>().color = Color.red;

        // async call to restart at checkpoint after 3 seconds
        RestartGameInThree();
    }

    void FixedUpdate()
    {
        // user is able to move only if he/she is not dead
        if (!mDeadState)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }

}
