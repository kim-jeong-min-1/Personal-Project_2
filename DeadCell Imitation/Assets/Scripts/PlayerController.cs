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
    private bool isWalking;
    private bool isGrounded = true;

    private float moveDirection;

    public float jumpForce;
    private float jumpCount;

    public float groundCheckRadious;
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
    }

    private void MovementDirectionCheck()
    {
        isRight = (moveDirection > 0) ? true : false;
        isWalking = (rb.velocity.x != 0) ? true : false;

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
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadious);

        if (isGrounded)
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


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadious);
    }
}
