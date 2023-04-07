using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermPressurePlate : MonoBehaviour
{
    public GameObject linkedObject;
    public int numOnPlate;


    public void RemoveObject()
    {
        Destroy(linkedObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player") || (collision.tag == "Summon"))
        {
            numOnPlate++;
            if (numOnPlate >= 1)
            {
                RemoveObject();
            }

        }
    }
}
