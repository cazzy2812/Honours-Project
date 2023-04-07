using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeObject : StateMachineBehaviour
{

    public float disappearTime = 1f;
    private float timePassed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject objectToRemove;
    Color color;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed = 0;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        objectToRemove = animator.gameObject;
        color = spriteRenderer.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed += Time.deltaTime;

        float alpha = color.a * (1-(timePassed / disappearTime));

        spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);

        if(timePassed > disappearTime)
        {
            Destroy(objectToRemove);
        }
    }

    
}
