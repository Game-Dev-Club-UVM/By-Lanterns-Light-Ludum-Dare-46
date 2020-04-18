﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovementGravityChange : MonoBehaviour
{
    //componets
    Rigidbody2D rb;
    CharacterController2D character;
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
    [SerializeField] private float fallMultiplier = 5f;
    [SerializeField] private float lowJumpMultiplier = 5f;
    [SerializeField] private float jumpingGravityMultiplier = 2.5f;
    [SerializeField] private float jumpForce = 600f;

    [SerializeField] private float powerJumpFallMultiplier = 4f;
    [SerializeField] private float powerJumpLowJumpMultiplier = 4f;
    [SerializeField] private float powerJumpJumpingGravityMultiplier = 1.5f;
    [SerializeField] private float powerJumpingJumpForce = 700f;

    [SerializeField] private bool powerJumping = false;

    //dashing
    [SerializeField] private DashState dashState;
    [SerializeField] private float dashTimer;
    [SerializeField] private float maxDash = 1f;
    [SerializeField] private float dashSpeed = 30f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<CharacterController2D>();
    }
    // Update is called once per frame
    private void Update()
    {

        //move horizontal 
        if (dashState != DashState.Dashing)
        {
            character.Move(Input.GetAxis("Horizontal"), Input.GetButton("Crouch"), Input.GetButtonDown("Jump"));
        }

        //dash
        switch (dashState)
        {
            case DashState.Ready:
                var isDashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
                if (isDashKeyDown && Input.GetAxis("Horizontal") != 0)
                {
                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        rb.velocity = new Vector2(dashSpeed, 0.75f);
                    }
                    else if (Input.GetAxis("Horizontal") < 0)
                    {
                        rb.velocity = new Vector2(-dashSpeed, 0.75f);
                    }
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                rb.velocity = new Vector2(rb.velocity.x, 0.75f);
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                //dashTimer -= Time.deltaTime * 3;
                // See if we need to add anything that should reset dashes or to allow multiple dashes
                if (isGrounded())
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }

        //power jump
        powerJumping = Input.GetKey(KeyCode.Z);
       
    }

    // Fixed Update called after Update every fixed frame
    void FixedUpdate()
    {
        //move horizontal 
        if (moveRequest)
        {
            rb.AddForce(Vector2.right * moveVelocity * Input.GetAxis("Horizontal"), ForceMode2D.Impulse);
            moveRequest = false;
        }
        //jump
        if (jumpRequest)
        {
            //rb.velocity += Vector2.up * jumpVelocity;
            if (powerJumping)
            {
                character.setJumpForce(powerJumpingJumpForce);
            } else
            {
                character.setJumpForce(jumpForce);
            }
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        //better gravity for jumping 
        if (powerJumping)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = powerJumpFallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = powerJumpLowJumpMultiplier;
            }
            else
            {
                rb.gravityScale = powerJumpJumpingGravityMultiplier;
            }
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = lowJumpMultiplier;
            }
            else
            {
                rb.gravityScale = jumpingGravityMultiplier;
            }
        }
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

    public void resetDash(bool reset)
    {
        if (reset)
        {
            dashState = DashState.Ready;
            dashTimer = 0;
        }
    } 
}

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}