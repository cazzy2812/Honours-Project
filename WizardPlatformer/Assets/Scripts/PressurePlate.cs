using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject linkedObject;
    public GameObject linkedObjectPrefab;
    public int numOnPlate;

    public Vector3 objectSpawn;

    public void TempRemoveObject()
    {
        objectSpawn = linkedObject.transform.position;

        Destroy(linkedObject);
    }

    public void ReturnObject()
    {
        GameObject returnedObject = Instantiate(linkedObjectPrefab, objectSpawn, linkedObjectPrefab.transform.rotation);
        linkedObject = returnedObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "Player") || (collision.tag == "Summon"))
        {
            numOnPlate++;
            if(numOnPlate >= 1)
            {
                TempRemoveObject();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Player") || (collision.tag == "Summon"))
        {
            numOnPlate--;
            if(numOnPlate == 0)
            {
                ReturnObject();
            }
            
        }
    }
}
