using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            animator.SetBool("isWalking", true);
        }else{
            animator.SetBool("isWalking",false);
        }
    }
}
