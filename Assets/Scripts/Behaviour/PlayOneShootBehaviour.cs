using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShootBehaviour : StateMachineBehaviour
{
    public AudioClip audioToPlay;
    public float volume = 1f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false, playLoop = false;
    public float playDelay = 0f;

    private float timeSinceEntered= 0f;
    private bool hasPlayDelaySound = false;

    private float timeSinceLoop = 0f;
    public float delayLoop = 0.2f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter) AudioSource.PlayClipAtPoint(audioToPlay, animator.gameObject.transform.position, volume);

        timeSinceEntered = 0f;
        timeSinceLoop = delayLoop;
        hasPlayDelaySound = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !hasPlayDelaySound)
        {
            timeSinceEntered += Time.deltaTime;
            if (timeSinceEntered >= playDelay)
            {
                AudioSource.PlayClipAtPoint(audioToPlay, animator.gameObject.transform.position, volume);
                hasPlayDelaySound = true;
            }
        }

        if (playLoop)
        {
            timeSinceLoop += Time.deltaTime;
            if (timeSinceLoop >= delayLoop)
            {
                AudioSource.PlayClipAtPoint(audioToPlay, animator.gameObject.transform.position, volume);
                timeSinceLoop = 0f;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit) AudioSource.PlayClipAtPoint(audioToPlay, animator.gameObject.transform.position, volume);
    }
}
