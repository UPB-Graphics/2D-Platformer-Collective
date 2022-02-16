using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody2D;

    private string currentAnimation;
    private Animator animator;

    private SkillsManager skillsManager;

    [SerializeField] private LayerMask jumpableGround;
    private bool isGrounded;

    private float xAxis;
    private float yAxis;

    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float jumpForce = 450;
    private bool isJumpPressed;
    private float nextAttackTime = 0.0f;
    private bool isAttackPressed;
    private bool isAttaking;

    [SerializeField] string playerAnimationName;
    private string PLAYER_IDLE;
    private string PLAYER_RUN;
    private string PLAYER_JUMP;
    private string PLAYER_FALL;
    private string PLAYER_ATTACK;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        PLAYER_IDLE = playerAnimationName + "_Idle";
        PLAYER_RUN = playerAnimationName + "_Run";
        PLAYER_JUMP = playerAnimationName + "_Jump";
        PLAYER_FALL = playerAnimationName + "_Fall";
        PLAYER_ATTACK = playerAnimationName + "_Attack";

        skillsManager = SkillsManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            xAxis = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xAxis = -1f;
        }
        else
        {
            xAxis = 0f;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (Time.time > nextAttackTime)
            {
                nextAttackTime = Time.time + skillsManager.skills[SkillsManager.FIREBALL_INDEX].cooldownTime;
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
                ChangeAnimationState(PLAYER_RUN);

            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }

        //Check if trying to jump 
        if (isJumpPressed && isGrounded && !isAttaking)
        {
            isJumpPressed = false;
            ChangeAnimationState(PLAYER_JUMP);
            rigidBody2D.AddForce(new Vector2(0, jumpForce));
        }
        else if (rigidBody2D.velocity.y < -.1f)
        {
            if (!isGrounded)
            {
                ChangeAnimationState(PLAYER_FALL);
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
                    ChangeAnimationState(PLAYER_ATTACK);
                }

                float delay = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("AttackComplete", delay);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void AttackComplete()
    {
        skillsManager.skills[SkillsManager.FIREBALL_INDEX].Activate(this);
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
