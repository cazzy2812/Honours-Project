using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpell : MonoBehaviour
{
    public Transform spellSpawn;
    public GameObject spellPrefab;

    public void shootSpell()
    {
        GameObject spell = Instantiate(spellPrefab, spellSpawn.position, spellPrefab.transform.rotation);
        spell.transform.localScale = new Vector3(spell.transform.localScale.x * transform.localScale.x > 0 ? 1 : -1, spell.transform.localScale.y, spell.transform.localScale.z);
    }
}
