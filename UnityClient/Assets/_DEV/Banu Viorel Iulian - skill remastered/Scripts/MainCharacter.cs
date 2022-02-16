using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody2D;

    private string currentAnimation;
    private Animator animator;

    [SerializeField] Transform projectiles;
    [SerializeField] GameObject sonicWavePrefab;

    [SerializeField] private LayerMask jumpableGround;
    private bool isGrounded;

    private float xAxis;
    private float yAxis;

    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpForce = 400;
    private bool isJumpPressed;
    private float nextAttackTime = 0.0f;
    private bool isAttackPressed;
    private bool isAttaking;

    [SerializeField] string characterAnimationName;
    private string MAINCHARACTER_IDLE;
    private string MAINCHARACTER_RUN;
    private string MAINCHARACTER_JUMP;
    private string MAINCHARACTER_FALL;
    private string MAINCHARACTER_ATTACK;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        MAINCHARACTER_IDLE = characterAnimationName + "_Idle";
        MAINCHARACTER_RUN = characterAnimationName + "_Run";
        MAINCHARACTER_JUMP = characterAnimationName + "_Jump";
        MAINCHARACTER_FALL = characterAnimationName + "_Fall";
        MAINCHARACTER_ATTACK = characterAnimationName + "_Attack";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            xAxis = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            xAxis = -1f;
        }
        else
        {
            xAxis = 0f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + sonicWavePrefab.GetComponent<SonicWave>().attackRateSec;
                isAttackPressed = true;
            }
        }
    }

    private void FixedUpdate()
    {
        //Check if player is on the ground
        bool hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

        //Check update movement based on input
        Vector2 velocity = new Vector2(0, rigidBody2D.velocity.y);

        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (xAxis < 0 && !isAttaking)
        {
            velocity.x = -runSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (xAxis > 0 && !isAttaking)
        {
            velocity.x = runSpeed;
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            velocity.x = 0;
        }

        if (isGrounded && !isAttaking)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(MAINCHARACTER_RUN);

            }
            else
            {
                ChangeAnimationState(MAINCHARACTER_IDLE);
            }
        }

        //Check if trying to jump 
        if (isJumpPressed && isGrounded && !isAttaking)
        {
            isJumpPressed = false;
            ChangeAnimationState(MAINCHARACTER_JUMP);
            rigidBody2D.AddForce(new Vector2(0, jumpForce));
        }
        else if (rigidBody2D.velocity.y < -.1f)
        {
            if (!isGrounded)
            {
                ChangeAnimationState(MAINCHARACTER_FALL);
            }
        }

        //Assign the new velocity to the rigidbody
        rigidBody2D.velocity = velocity;

        //Attack
        if (isAttackPressed && xAxis == 0 && !isAttaking)
        {
            isAttackPressed = false;

            if (!isAttaking)
            {
                isAttaking = true;

                if (isGrounded)
                {
                    ChangeAnimationState(MAINCHARACTER_ATTACK);
                }

                float delay = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("AttackComplete", delay);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void GenerateProjectile()
    {
        Vector3 position = Vector3.zero;

        if (transform.localScale.x == -1)
        {
            position = new Vector3(transform.position.x - 0.5f, transform.position.y, 0);
            GameObject newProjectile = Instantiate(sonicWavePrefab, position, Quaternion.identity);
            newProjectile.GetComponent<SpriteRenderer>().flipX = true;
            newProjectile.GetComponent<SonicWave>().SetParent(gameObject.name);
            newProjectile.GetComponent<SonicWave>().SetDirection(true);
            newProjectile.transform.SetParent(projectiles);
        }
        else
        {
            position = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);
            GameObject newProjectile = Instantiate(sonicWavePrefab, position, Quaternion.identity);
            newProjectile.GetComponent<SpriteRenderer>().flipX = false;
            newProjectile.GetComponent<SonicWave>().SetParent(gameObject.name);
            newProjectile.GetComponent<SonicWave>().SetDirection(false);
            newProjectile.transform.SetParent(projectiles);
        }
    }

    private void AttackComplete()
    {
        isAttaking = false;
    }

    private void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimation == newAnimation)
        {
            return;
        }

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }
}
