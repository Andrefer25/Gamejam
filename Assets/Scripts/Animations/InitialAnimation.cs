using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialAnimation : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void EndAnimation()
    {
        animator.SetBool("initialEnded", true);
    }
}
