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

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            animator.SetBool("lanternFloat", true);
        } 
        else
        {
            animator.SetBool("lanternFloat", false);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("dashing", true);
        }
        else
        {
            animator.SetBool("dashing", false);
        }

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
            animator.SetBool("dashing", false);
            //animator.SetBool("onGround", false);
        }
        // Run animations
        else if (Input.GetAxis("Horizontal") != 0 && !charController.getCrouched() && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("dashing", false);
        } 
        else if (Input.GetAxis("Horizontal") != 0 && charController.getCrouched() && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("dashing", false);
        }
        // Walk animation transistions
        else if (Input.GetAxis("Horizontal") == 0 && !charController.getCrouched() && animator.GetBool("onGround")) 
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("dashing", false);
        }
        else if (Input.GetAxis("Horizontal") == 0 && charController.getCrouched() && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("dashing", false);
        }
        // Idle animations
        else if (!charController.getCrouched() && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("dashing", false);
        }
        else if (charController.getCrouched() && animator.GetBool("onGround"))
        {
            animator.SetBool("isCrouching", true);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("dashing", false);
        }
    }
}
