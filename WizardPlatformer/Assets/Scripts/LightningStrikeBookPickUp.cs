using Assets.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeBookPickUp : MonoBehaviour
{
    public Vector3 rotation = new Vector3(0, 90, 0);
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        playerController.PickUpLightningStrike();
        CharacterEvents.provideInfo.Invoke(gameObject, "Press Z to cast lightning strike!");
        Destroy(gameObject);
    }

    public void Update()
    {
        transform.eulerAngles += rotation * Time.deltaTime;
    }
}
