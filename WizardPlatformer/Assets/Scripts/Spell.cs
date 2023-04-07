using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public int spellDamage = 10;
    public Vector2 spellSpeed = new Vector2(10f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    public float spellDestroyTime = 0.5f;

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Invoke("deleteSpell", 3);
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            rb.velocity = new Vector2(spellSpeed.x * transform.localScale.x, spellSpeed.y);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        if(damage != null )
        {
            Vector2 properKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool attackHit = damage.Hit(spellDamage, properKnockback);

            if (attackHit)
            {
                Debug.Log(collision.name + " hit for " + spellDamage);
                animator.SetTrigger(AnimationStrings.spellHitTrigger);
                Invoke("deleteSpell", spellDestroyTime);
            }
        }
    }

    private void deleteSpell()
    {
        Destroy(gameObject);
    }
}
