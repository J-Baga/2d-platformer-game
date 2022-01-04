using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask ground, goal;
    [SerializeField] private GameObject toasterHoriz, toasterVert;

    private CircleCollider2D collToasterHoriz, collToasterVert;
    private Rigidbody2D rbToasterHoriz, rbToasterVert;
    private Animator animHoriz, animVert;
    private Transform tfHoriz, tfVert;

    private AudioManager playerMovementAudio;
    private Player playerState;

    private float movementSpeed = 4;
    private float jumpForce = 5.5f;
    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight;

    private void Start()
    {
        this.enabled = true;
        collToasterHoriz = toasterHoriz.GetComponent<CircleCollider2D>();
        collToasterVert = toasterVert.GetComponent<CircleCollider2D>();
        rbToasterHoriz = toasterHoriz.GetComponent<Rigidbody2D>();
        rbToasterVert = toasterVert.GetComponent<Rigidbody2D>();
        animHoriz = toasterHoriz.GetComponent<Animator>();
        animVert = toasterVert.GetComponent<Animator>();
        tfHoriz = toasterHoriz.GetComponent<Transform>();
        tfVert = toasterVert.GetComponent<Transform>();
        isFacingRight = true;

        playerMovementAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        playerState = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Check if player is standing on the ground.
        isGrounded = CheckGrounded(collToasterHoriz) || CheckGrounded(collToasterVert);

        // Check if player is standing on the goal.
        if (IsTouchingGoal(collToasterHoriz) || IsTouchingGoal(collToasterVert))
        {
            rbToasterHoriz.velocity = new Vector2(0.25f, 1);
            rbToasterVert.velocity = new Vector2(0.25f, 1);
            this.enabled = false;
        }
        
        if (playerState.isDead)
        {
            rbToasterHoriz.velocity = new Vector2(0, 0);
            rbToasterVert.velocity = new Vector2(0, 0);
            this.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        rbToasterHoriz.velocity = new Vector2(horizontalInput * movementSpeed, rbToasterHoriz.velocity.y);
        rbToasterVert.velocity = new Vector2(horizontalInput * movementSpeed, rbToasterVert.velocity.y);

        // Determine whether player is moving right or left, change orientation based on where it's currently facing.
        if (horizontalInput > 0 && isFacingRight == false)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight == true)
        {
            Flip();
        }

        // Player can only jump when touching the ground first.
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            playerMovementAudio.playerJumping.Play();
            Jump();
        }

        animHoriz.SetBool("isMoving", horizontalInput != 0);
        animVert.SetBool("isMoving", horizontalInput != 0);
    }

    private void Jump()
    {
            rbToasterHoriz.velocity = new Vector2(rbToasterHoriz.velocity.x, jumpForce);
            rbToasterVert.velocity = new Vector2(rbToasterVert.velocity.x, jumpForce);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        tfHoriz.Rotate(0f, 180f, 0f);
        tfVert.Rotate(0f, 180f, 0f);
    }

    private bool CheckGrounded(CircleCollider2D coll)
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    private bool IsTouchingGoal(CircleCollider2D coll)
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, goal);
    }
}


