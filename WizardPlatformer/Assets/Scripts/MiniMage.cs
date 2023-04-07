using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMage : MonoBehaviour
{
    public float speed = 5f;
    public float timeLimit = 20f;

    public DetectionArea attackArea;
    public DetectionArea edgeDetection;

    Rigidbody2D rb;
    TouchingSurface touchingSurface;
    Animator animator;
    Damage damage;

    public enum WalkDirection { Left, Right }

    private WalkDirection direction;
    private Vector2 directionVector = Vector2.right;

    public WalkDirection Direction
    {
        get
        {
            return direction;
        }
        set
        {
            if (direction != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkDirection.Right)
                {
                    directionVector = Vector2.left;
                }
                else if (value == WalkDirection.Left)
                {
                    directionVector = Vector2.right;
                }
            }
            direction = value;
        }
    }

    private bool lockMovement = false;
    public bool LockMovement
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockMovement);
        }
        set
        {
            LockMovement = value;
            animator.SetBool(AnimationStrings.lockMovement, lockMovement);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingSurface = GetComponent<TouchingSurface>();
        animator = GetComponent<Animator>();
        damage = GetComponent<Damage>();
    }

    private void FixedUpdate()
    {
        if (touchingSurface.IsGrounded && (touchingSurface.IsTouchingWall || edgeDetection.collidersDetected.Count == 0))
        {
            FlipDirection();
        }

        if (!LockMovement)
        {
            if (CanMove && touchingSurface.IsGrounded)
            {
                rb.velocity = new Vector2(speed * directionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }


    }

    private void FlipDirection()
    {
        if (Direction == WalkDirection.Right)
        {
            Direction = WalkDirection.Left;
        }
        else if (Direction == WalkDirection.Left)
        {
            Direction = WalkDirection.Right;
        }
        else
        {
            Debug.LogError("Current direction isn't left or right");
        }
    }

    public bool targetInRange = false;
    public bool TargetInRange
    {
        get
        {
            return targetInRange;
        }
        private set
        {
            targetInRange = value;
            animator.SetBool(AnimationStrings.targetInRange, targetInRange);
        }
    }

    public bool canMove = true;
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
        private set
        {
            canMove = value;
            animator.SetBool(AnimationStrings.canMove, canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        TargetInRange = attackArea.collidersDetected.Count > 0;
        AttackCooldown -= Time.deltaTime;
        timeLimit -= Time.deltaTime;

        if(timeLimit <= 0)
        {
            damage.CurrentHP = 0;
        }

        if (isTouchingSpike)
        {
            damage.Hurt(50);
        }

    }

    public void OnHit(int damageOfAttack, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public bool isTouchingSpike;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objectCollided = collision.gameObject;

        if (objectCollided.CompareTag("Spike"))
        {
            isTouchingSpike = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject objectCollided = collision.gameObject;

        if (objectCollided.CompareTag("Spike"))
        {
            isTouchingSpike = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PressurePlate")
        {
            CanMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PressurePlate")
        {
            CanMove = true;
        }
    }
}
