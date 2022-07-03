using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private bool isRight = true;
    private bool isWalking;
    private float moveDirection;

    public float moveSpeed;
    public float jumpForce;
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

    private void PlayerMovement()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
