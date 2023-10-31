using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    // Time it take to fade the object
    public float fadeTime = 3f;
    // Time before starting the fade effect
    public float fadeDelay = 1f;

    private float fadeDelayElapsed = 0f;
    private float timeElapsed = 0f;

    SpriteRenderer spriteRender;
    GameObject objToRemove;
    Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        fadeDelayElapsed = 0f;
        spriteRender = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRender.color;
        objToRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fadeDelay > fadeDelayElapsed) fadeDelayElapsed += Time.deltaTime;
        else
        {
            timeElapsed += Time.deltaTime;

            // La transparence va baisser au fur et à mesure du temps
            float newAlpha = startColor.a * (1 - (timeElapsed / fadeTime));

            spriteRender.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            if (timeElapsed > fadeTime) Destroy(objToRemove);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
