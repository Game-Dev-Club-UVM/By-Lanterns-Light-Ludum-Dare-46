using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Run animation
        if (Input.GetAxis("Horizontal") != 0 && !Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isRunning", true);
        } 
        else if (!Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", false);
            animator.SetBool("isRunning", false);
        } 
        else if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", true);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isCrouching", true);
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("jump");
        }
    }
}
