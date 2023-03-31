using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invsible : StateMachineBehaviour
{
    private SpriteRenderer spriteRenderer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }
}