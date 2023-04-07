using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMiniMage : MonoBehaviour
{
    public Transform miniMageSpawn;
    public GameObject miniMagePrefab;

    public void summonMiniMage()
    {
        GameObject miniMage = Instantiate(miniMagePrefab, miniMageSpawn.position, miniMagePrefab.transform.rotation);
        miniMage.transform.localScale = new Vector3(miniMage.transform.localScale.x, miniMage.transform.localScale.y, miniMage.transform.localScale.z);
    }
}
