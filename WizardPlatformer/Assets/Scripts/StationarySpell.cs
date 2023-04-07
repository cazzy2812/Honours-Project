using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationarySpell : MonoBehaviour
{

    public int spellDamage = 10;
    public Vector2 knockback = new Vector2(0, 0);
    public float spellDestroyTime = 0.5f;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Invoke("deleteSpell", spellDestroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Damage damage = collision.GetComponent<Damage>();

        if (damage != null)
        {
            Vector2 properKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool attackHit = damage.Hit(spellDamage, properKnockback);

            if (attackHit)
            {
                Debug.Log(collision.name + " hit for " + spellDamage);
            }
        }

    }

    private void deleteSpell()
    {
        Destroy(gameObject);
    }
}
