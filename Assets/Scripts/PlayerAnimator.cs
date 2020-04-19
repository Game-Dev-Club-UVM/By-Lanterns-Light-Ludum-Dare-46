using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    bool onGround = true;
    CharacterController2D charController;

    public void touchGround()
    {
        onGround = true;
        animator.SetBool("onGround", true);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        charController = GetComponentInParent<CharacterController2D>();
    }

    void Update()
    {
        // Check if on ground
        onGround = charController.getGrounded();
        animator.SetBool("onGround", onGround);

        // Jumping trigger animations
        if (Input.GetButtonDown("Jump"))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("powerJump", true);
            }
            else
            {
                animator.SetBool("jump", true);
            }
            onGround = false;
            animator.SetBool("onGround", false);
        }
        // Run animations
        else if (Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.LeftControl) && onGround)
        {
            animator.SetBool("powerJump", false);
            animator.SetBool("jump", false);
            animator.SetBool("isCrouching", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        } 
        else if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.LeftControl) && onGround)
        {
            animator.SetBool("powerJump", false);
            animator.SetBool("jump", false);
            animator.SetBool("isCrouching", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
        // Walk animation transistions
        else if (Input.GetAxis("Horizontal") == 0 && !Input.GetKey(KeyCode.LeftControl) && onGround) {
            animator.SetBool("powerJump", false);
            animator.SetBool("jump", false);
            animator.SetBool("isCrouching", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetKey(KeyCode.LeftControl) && onGround)
        {
            animator.SetBool("powerJump", false);
            animator.SetBool("jump", false);
            animator.SetBool("isCrouching", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
        }
        // Idle animations
        else if (!Input.GetKey(KeyCode.LeftControl) && onGround)
        {
            animator.SetBool("powerJump", false);
            animator.SetBool("jump", false);
            animator.SetBool("isCrouching", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && onGround)
        {
            animator.SetBool("powerJump", false);
            animator.SetBool("jump", false);
            animator.SetBool("isCrouching", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }
}
