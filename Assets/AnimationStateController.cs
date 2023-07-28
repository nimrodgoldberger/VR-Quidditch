using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    //Animator animator;
    //float velocity = 0.0f;
    //public float acceleration = 0.1f;
    //int VelocityHash;

    // Start is called before the first frame update
    //void Start()
    //{
    //    //set reference for animator
    //    animator = GetComponent<Animator>();

    //    //to increase performance
    //    VelocityHash = Animator.StringToHash("Velocity");


    //}

    //void Update()
    //{

    //    bool forwardPressed = Input.GetKey("w");

    //    if(forwardPressed)
    //    {
    //        velocity += Time.deltaTime * acceleration;
    //        animator.SetFloat(VelocityHash, velocity);
    //    }

    //    //animator.SetFloat(VelocityHash, velocity);

    //}


    Animator animator;
    int isWalkingHash;
    int isCrouchWalkingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isCrouchWalkingHash = Animator.StringToHash("isCrouchWalking");
    }

    void FixedUpdate()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isCrouchWalking = animator.GetBool(isCrouchWalkingHash);

        bool forwardPressed = Input.GetKey("w");
        bool isCrouchPressed = Input.GetKey("q");


        if(!isWalking && forwardPressed)
        {
            animator.SetBool("isWalking", true);

        }
        else if(isWalking && !forwardPressed)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isCrouchWalking", false);
        }

        if(!isCrouchWalking && (isCrouchPressed && forwardPressed))
        {
            animator.SetBool("isCrouchWalking", true);
        }
        if(isCrouchWalking && (!isCrouchPressed || !forwardPressed))
        {
            animator.SetBool("isCrouchWalking", false);
        }

        //if(isWalking && !isCrouchWalking && wasCrouchPressed)
        //{
        //    animator.SetBool("isCrouchWalking", true);
        //}
        //else if(!isWalking || (isCrouchWalking && wasCrouchPressed) )
        //{
        //    animator.SetBool("isCrouchWalking", false);
        //}

        //animator.SetBool("IsCrouchWalking", forwardPressed);

    }
}
