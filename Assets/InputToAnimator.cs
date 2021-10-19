using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToAnimator : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("horizontal_input", Input.GetAxis("Horizontal"));
        animator.SetFloat("vertical_input", Input.GetAxis("Vertical"));

        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.speed = 0.0f;
        }
        else
        {
            animator.speed = 1.0f;
        }
    }
}
