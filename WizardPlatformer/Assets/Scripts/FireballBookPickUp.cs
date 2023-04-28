using Assets.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBookPickUp : MonoBehaviour
{
    public Vector3 rotation = new Vector3(0, 90, 0);
    public AudioClip sfx;
    public float volume = 1f;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        playerController.PickUpFireball();
        CharacterEvents.provideInfo.Invoke(gameObject, "Press Q to cast fireball!");
        AudioSource.PlayClipAtPoint(sfx, gameObject.transform.position, volume);
        Destroy(gameObject);
    }

    public void Update()
    {
        transform.eulerAngles += rotation * Time.deltaTime;
    }
}
