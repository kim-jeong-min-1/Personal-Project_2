using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStet
{
    public int HP;
    public int AtkDmg;
    public float Speed;
    public float JumpCount;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    PlayerStet playerStet;

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField]
    private Transform groundCheckPoint;

    public LayerMask ground;

    private bool isRight = true;
    private bool isGrounded = true;
    private bool isWalking;
    private bool isRolling;

    [SerializeField]
    private float groundCheckRadious;
    private float moveDirection;
    private int playerFoward;

    public float jumpForce;
    private float jumpCount;

    public float dashSpeed;
    public float dashCoolDown;
    private float lastdash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
        MovementDirectionCheck();
        AnimationsCheck();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void InputSystem()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Rolling();
        }
    }

    private void MovementDirectionCheck()
    {
        isRight = (moveDirection > 0) ? true : false;
        isWalking = (rb.velocity.x != 0 && !isRolling) ? true : false;
        playerFoward = (isRight) ? 1 : -1;

        if (moveDirection == 0) return;

        if(!isRight)
        {
            transform.rotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
        }
        else if(isRight)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void AnimationsCheck()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isRolling", isRolling);
    }
    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadious, ground);

        if (isGrounded && rb.velocity.y <= 0)
        {
            jumpCount = playerStet.JumpCount;
        }
    }

    private void PlayerMovement()
    {
        rb.velocity = new Vector2(moveDirection * playerStet.Speed, rb.velocity.y);
    }

    private void Jump()
    {
        if(jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }
    }

    #region ±¸¸£±â
    private void Rolling()
    {
        if(Time.time < lastdash + dashCoolDown) return;

        isRolling = true;
        lastdash = Time.time;
        StartCoroutine(RollingSystem());
    }
    private IEnumerator RollingSystem()
    {
        rb.velocity = new Vector2(dashSpeed * playerFoward, rb.velocity.y);
        yield return new WaitForSeconds(0.75f);
        isRolling = false;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadious);
    }
}
