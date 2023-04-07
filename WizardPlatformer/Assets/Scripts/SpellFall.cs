using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellFall : MonoBehaviour
{
    public Vector3 spawn;

    public Transform noTargetSpawn;
    public GameObject spellPrefab;
    public bool enemyInRange = false;

    public void spellFall()
    {
        if (!enemyInRange)
        {
            spawn = noTargetSpawn.position;
        }
        GameObject spell = Instantiate(spellPrefab, spawn, spellPrefab.transform.rotation);
        spell.transform.localScale = new Vector3(spell.transform.localScale.x * transform.localScale.x > 0 ? 1 : -1, spell.transform.localScale.y, spell.transform.localScale.z);
        enemyInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            GameObject gameObject = collision.gameObject;

            spawn = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            enemyInRange = true;
        }   
    }    
}
