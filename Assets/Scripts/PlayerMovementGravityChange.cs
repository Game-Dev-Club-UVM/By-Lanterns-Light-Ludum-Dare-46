using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Oil))]
public class PlayerMovementGravityChange : MonoBehaviour
{
    //componets
    Rigidbody2D rb;
    CharacterController2D character;
    //Ground Layers
    [SerializeField] private LayerMask groundLayer = 9;

    //movement speeds
    [Range(0, 1)]
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

    //oil
    Oil oilMeter;
    [SerializeField] private int dashOilCost = 10;
    [SerializeField] private int powerJumpOilCost = 10;
    [SerializeField] private float lanternDashTimer = 0f;
    [SerializeField] private float maxLanternDash = 3f;
    [SerializeField] private bool cooldown = false;

    //lantern jump
    [SerializeField] private int lanternDashOilCost = 1;
    [SerializeField] private float lanternDashSpeed = 15f;

    //powers
    [SerializeField] private bool dash = false;
    [SerializeField] private bool powerJump = false;
    [SerializeField] private bool pull = false;

    //wall jumping
    // TODO: Implement wall jumping, figure out a nice way that they can't use the same wall over and over again
    // unless we want it to look very Super Meatboy like with wall jumping and sliding but that doesn't seem needed
    [SerializeField] private bool wallJumpEnabled = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<CharacterController2D>();
        oilMeter = GameObject.Find("Lantern").GetComponent<Oil>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (oilMeter.getOil() > 0)
        {
            //move horizontal 
            if (dashState != DashState.Dashing && !Input.GetKeyDown(KeyCode.F) || cooldown)
            {
                character.Move(Input.GetAxis("Horizontal"), Input.GetButton("Crouch"), Input.GetButtonDown("Jump"));
            }

            //dash
            if (dash)
            {
                switch (dashState)
                {
                    case DashState.Ready:
                        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetAxis("Horizontal") != 0)
                        {
                            oilMeter.removeOil(dashOilCost);
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
            }

            //lantern dash
            if (pull)
            {
                if (Input.GetKey(KeyCode.F) && !cooldown && lanternDashTimer <= maxLanternDash)
                {
                    oilMeter.removeOil(lanternDashOilCost * Time.deltaTime);
                    rb.velocity = (oilMeter.gameObject.transform.position - transform.position).normalized * lanternDashSpeed;
                    lanternDashTimer += Time.deltaTime;
                }
                else if (lanternDashTimer >= maxLanternDash && !cooldown)
                {
                    cooldown = true;
                }
                else if (lanternDashTimer > 0)
                {
                    lanternDashTimer -= Time.deltaTime;
                }
                else
                {
                    cooldown = false;
                }
            }

            //power jump
            if (powerJump)
            {
                powerJumping = Input.GetKey(KeyCode.LeftControl);
            }

        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
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
            if (rb.velocity.y == 0 && Input.GetButtonDown("Jump"))
            {
                oilMeter.removeOil(powerJumpOilCost);
            }

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
        if (reset && dashTimer == maxDash)
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