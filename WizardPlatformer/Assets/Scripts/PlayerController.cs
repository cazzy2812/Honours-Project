using Assets.Scripts.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(TouchingSurface))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float jumpSpeed = 10.5f;
    public float airSpeed = 10f;
    Vector2 moveInput;
    TouchingSurface touchingSurface;
    Damage damage;

    
    public Vector3 respawnPoint;
    public GameObject fallDetection;

    public bool hasFireballBook = false;
    public bool hasMiniMageScroll = false;
    public bool hasLightningStrikeBook = false;
    public float currentSpeed
    {
        get
        {
            if (CanMove)
            {
                if (isMoving && !touchingSurface.IsTouchingWall)
                {
                    if (touchingSurface.IsGrounded)
                    {
                        return walkSpeed;
                    }
                    else
                    {
                        return airSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            
        }
    }


    private bool isMoving = false;

    public bool IsMoving {
        get
        {
            return isMoving;
        }
        private set
        {
            isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, isMoving);
        }
    }

    public bool isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
        private set
        {
            if (isFacingRight != value)
            {
                // Flipping the local scale so everything relating to player switches direction
                transform.localScale *= new Vector2(-1, 1);
            }
            isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
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

    Rigidbody2D rb;
    Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingSurface = GetComponent<TouchingSurface>();
        respawnPoint = transform.position;
        damage = GetComponent<Damage>();
    }

    private void FixedUpdate()
    {
        if (!LockMovement)
        {
            rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);
        }
        

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            setDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
        
    }

    private void setDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            // Face right
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            // Face left
            IsFacingRight = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //TODO check if player is alive and has double jump
        if (context.started && touchingSurface.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        else if(context.started && CanMove && (touchingSurface.airJumps >= 1))
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            touchingSurface.airJumps--;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnFireSpell(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (hasFireballBook)
            {
                animator.SetTrigger(AnimationStrings.fireSpellTrigger);
            }
            else
            {
                CharacterEvents.provideInfo.Invoke(gameObject, "You do not have that spell yet!");
            }
            
        }
    }

    private float summonCooldown = 0f;

    public float maxSummonCooldown = 20f;
    public void OnSummonMiniMage(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (hasMiniMageScroll)
            {
                if (summonCooldown <= 0)
                {
                    animator.SetTrigger(AnimationStrings.miniMageTrigger);
                    summonCooldown = maxSummonCooldown;
                }
                else
                {
                    CharacterEvents.provideInfo.Invoke(gameObject, "Can only summon mini mage once every 20 seconds!");
                }
            }
            else
            {
                CharacterEvents.provideInfo.Invoke(gameObject, "You do not have that summon yet!");
            }
        }
    }

    private float lightningCooldown = 0f;
    public float maxLightningCooldown = 10f;
    public void OnLightningStrikeSpell(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (hasLightningStrikeBook)
            {
                if (lightningCooldown <= 0)
                {
                    animator.SetTrigger(AnimationStrings.lightningStrikeTrigger);
                    lightningCooldown = maxLightningCooldown;
                }
                else
                {
                    CharacterEvents.provideInfo.Invoke(gameObject, "Can only summon lightning once every 10 seconds!");
                }
            }
            else
            {
                CharacterEvents.provideInfo.Invoke(gameObject, "You do not have that spell yet!");
            }
        }
    }

    public void OnHit(int damageOfAttack, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }

    public void PickUpExtraJump()
    {
        touchingSurface.maxAirJumps++;
    }

    public void PickUpFireball()
    {
        hasFireballBook = true;
    }

    public void PickUpMiniMage()
    {
        hasMiniMageScroll = true;
    }

    public void PickUpLightningStrike()
    {
        hasLightningStrikeBook = true;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        damage.IsAlive = true;
        damage.CurrentHP = damage.MaxHP;
        respawnTime = 3f;
        
    }

    public void RespawnNow()
    {
        damage.CurrentHP = damage.MaxHP;
        damage.IsAlive = true;
        transform.position = respawnPoint;
    }

    public int currentCheckpoint = 0;

    public bool NewCheckpoint(Vector3 checkpoint, int checkpointNumber)
    {
        if(checkpointNumber > currentCheckpoint)
        {
            respawnPoint = checkpoint;
            currentCheckpoint = checkpointNumber;

            return true;
        }
        return false;
    }

    public float respawnTime = 3f;

    private void Update()
    {
        if(summonCooldown >= 0)
        {
            summonCooldown -= Time.deltaTime;
        }
        if(lightningCooldown >= 0)
        {
            lightningCooldown -= Time.deltaTime;
        }
        if (!damage.IsAlive)
        {
            respawnTime -= Time.deltaTime;
            if(respawnTime <= 0)
            {
                isTouchingSpike = false;
                Respawn();
            }
        }

        if (isTouchingSpike)
        {
            damage.Hurt(50);
        }

        fallDetection.transform.position = new Vector2(transform.position.x, fallDetection.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetection")
        {
            RespawnNow();
        }
        
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
}
