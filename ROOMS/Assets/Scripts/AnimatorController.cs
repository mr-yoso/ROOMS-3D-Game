using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator animator;
    PlayerController movement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerController>();
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
