using Assets.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> hpUpdate;

    Animator animator;

    [SerializeField]
    private int maxHP = 100;
    public int MaxHP
    {
        get
        {
            return maxHP;
        }
        set
        {
            maxHP = value;
        }
    }

    [SerializeField]
    private int currentHP = 100;

    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
            hpUpdate?.Invoke(currentHP, maxHP);

            if (currentHP <= 0) {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool isDamageable = true;
    private bool isDamageableByTrap = true;
    private float timeSinceDamaged = 0;
    private float timeSinceTrap = 0;
    public float hitProtectionTime = 0.25f;
    public float trapProtectionTime = 1f;

    [SerializeField]
    private bool isAlive = true;

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
        set
        {
            isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, isAlive);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && isDamageable)
        {
            CurrentHP -= damage;
            isDamageable = false;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            damageableHit?.Invoke(damage, knockback);

            CharacterEvents.damageTaken.Invoke(gameObject, damage);

            return true;
        }

        return false;
    }

    public bool Hurt(int damage)
    {
        if (IsAlive && isDamageableByTrap) 
        {
            CurrentHP -= damage;
            isDamageableByTrap = false;

            CharacterEvents.damageTaken.Invoke(gameObject, damage);

            return true;
        }

        return false;
    }

    public void Heal(int healAmount)
    {
        if (IsAlive)
        {
            int potentialHeal = Mathf.Max(MaxHP - CurrentHP, 0);
            int actualHeal = Mathf.Min(potentialHeal, healAmount);
            CurrentHP += actualHeal;

            CharacterEvents.hpHealed.Invoke(gameObject, actualHeal);
        }
    }

    public void Update()
    {
        if (!isDamageable)
        {
            if (timeSinceDamaged > hitProtectionTime)
            {
                isDamageable = true;
                timeSinceDamaged = 0;
            }

            timeSinceDamaged += Time.deltaTime;
        }
        if (!isDamageableByTrap)
        {
            if(timeSinceTrap > trapProtectionTime)
            {
                isDamageableByTrap = true;
                timeSinceTrap = 0;
            }
            timeSinceTrap += Time.deltaTime;
        }

        
    }
}
