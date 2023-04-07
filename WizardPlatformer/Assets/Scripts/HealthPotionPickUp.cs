using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionPickUp : MonoBehaviour
{

    public int healAmount = 50;
    public Vector3 rotation = new Vector3(0, 90, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        if (damage)
        {
            damage.Heal(healAmount);
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        transform.eulerAngles += rotation * Time.deltaTime;
    }
}
