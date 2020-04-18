using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    //componets
    Rigidbody2D rb;
    //Ground Layers
    [SerializeField] private LayerMask groundLayer = 9;

    //movement speeds
    [Range(0,1)]
    [SerializeField] private float moveVelocity = 1;
    [Range(1, 10)]
    [SerializeField] private float jumpVelocity = 5;

    //input request
    bool moveRequest = false;
    bool jumpRequest = false;

    //better gravity variables
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        //move horizontal 
        if (Input.GetAxis("Horizontal") != 0)
        {
            moveRequest = true;
        }
        //jump
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpRequest = true;
        }
        //dash
    }

    // Fixed Update called after Update every fixed frame
    void FixedUpdate()
    {
        //move horizontal 
        if (moveRequest)
        {
            rb.AddForce(Vector2.right * moveVelocity * Input.GetAxis("Horizontal") , ForceMode2D.Impulse);
            moveRequest = false;
        }
        //jump
        if (jumpRequest)
        {
            //rb.velocity += Vector2.up * jumpVelocity;
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        //better gravity for jumping 
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
        //dash

    }

    bool isGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
