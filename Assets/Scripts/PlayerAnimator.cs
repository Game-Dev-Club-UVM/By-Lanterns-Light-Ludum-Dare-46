using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    CharacterController2D charController;

    public void touchGround()
    {
        animator.SetBool("onGround", true);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        charController = GetComponentInParent<CharacterController2D>();
    }

    void FixedUpdate()
    {
        // Check if on ground
        animator.SetBool("onGround", charController.getGrounded());
        if (animator.GetBool("onGround"))
        {
            animator.SetBool("jump", false);
            animator.SetBool("powerJump", false);
        }

        // Jumping trigger animations
        if (Input.GetButton("Jump") && animator.GetBool("onGround"))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("powerJump", true);
            }
            else
            {
                animator.SetBool("jump", true);
            }
            //animator.SetBool("onGround", false);
        }
        // Run animations
        else if (Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.LeftControl) && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        } 
        else if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.LeftControl) && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
        }
        // Walk animation transistions
        else if (Input.GetAxis("Horizontal") == 0 && !Input.GetKey(KeyCode.LeftControl) && animator.GetBool("onGround")) 
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetKey(KeyCode.LeftControl) && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
        }
        // Idle animations
        else if (!Input.GetKey(KeyCode.LeftControl) && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }
}
