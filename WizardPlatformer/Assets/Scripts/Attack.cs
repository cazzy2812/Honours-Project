using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    Collider2D attackCollider;

    public int damageOfAttack = 20;
    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
        attackCollider= GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        if(damage != null)
        {
            Vector2 properKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool attackHit = damage.Hit(damageOfAttack, properKnockback);

            if (attackHit)
            {
                Debug.Log(collision.name + " hit for " + damageOfAttack);
            }
            
        }
    }

    
}
