using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        // animator.SetFloat("CharacterSpeed", movement.GetAnimationSpeed());
    }
}
