using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingSurface : MonoBehaviour
{

    Rigidbody2D rb;
    CapsuleCollider2D touchingSurface;
    Animator animator;
    BoxCollider2D touchingPlatform;

    public ContactFilter2D contactFilter;
    public ContactFilter2D platformFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    public float airJumps = 0;
    public float maxAirJumps = 0;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    RaycastHit2D[] platformHits = new RaycastHit2D[5];

    private bool isGrounded;

    public bool IsGrounded { 
        get 
        {
            return isGrounded;
        } 
        private set 
        { 
            isGrounded= value;
            animator.SetBool(AnimationStrings.isGrounded, isGrounded);
            if(isGrounded == true)
            {
                airJumps = maxAirJumps;
            }

        } 
    }

    private bool isTouchingWall;
    private Vector2 wallDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsTouchingWall
    {
        get
        {
            return isTouchingWall;
        }
        private set
        {
            isTouchingWall = value;
            animator.SetBool(AnimationStrings.isTouchingWall, isTouchingCeiling);
        }
    }

    private bool isTouchingCeiling;

    public bool IsTouchingCeiling
    {
        get
        {
            return isTouchingCeiling;
        }
        private set
        {
            isTouchingCeiling = value;
            animator.SetBool(AnimationStrings.isTouchingCeiling, isTouchingCeiling);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingSurface= rb.GetComponent<CapsuleCollider2D>();
        touchingPlatform = rb.GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    

    private void FixedUpdate()
    {

        IsGrounded = (touchingSurface.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0) || (touchingPlatform.Cast(Vector2.down, platformFilter, platformHits, groundDistance) > 0);
        IsTouchingWall = touchingSurface.Cast(wallDirection, contactFilter, wallHits, wallDistance) > 0;
        IsTouchingCeiling = touchingSurface.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
