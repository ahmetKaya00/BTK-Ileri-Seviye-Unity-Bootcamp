using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
       animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.R)){
            animator.SetBool("isScale",true);
        }
        else if(Input.GetKey(KeyCode.E)){
            animator.SetBool("isRotate",true);
        }
    }
}
