using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public DashState dashState;
    public float dashTimer;
    public float maxDash = 1f;
    public float dashSpeed = 30f;
    public float dashLength = 3f;
    private Rigidbody2D rb;

    public Vector2 savedVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        switch (dashState)
        {
            case DashState.Ready:
                var isDashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
                if (isDashKeyDown)
                {
                    savedVelocity = new Vector2(rb.velocity.x, 0.01f);

                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        rb.velocity = new Vector2(dashSpeed, 0.01f);
                    }
                    else if (Input.GetAxis("Horizontal") < 0)
                    {
                        rb.velocity = new Vector2(-dashSpeed, 0.01f);
                    }
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    rb.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }
}