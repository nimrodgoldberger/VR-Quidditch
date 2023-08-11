using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
    }

    public void SetIdleAnimation()
    {
        animator.SetBool("Idle", true);
    }

    public void SetCelebrationAnimation()
    {
        animator.SetBool("Winner", true);
    }

    public void SetLossAnimation()
    {
        animator.SetBool("Loser", true);
    }
}
