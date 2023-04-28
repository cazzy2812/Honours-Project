using Assets.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Checkpoint : MonoBehaviour
{

    public int checkpointNumber;
    public Vector3 checkpoint;
    public bool spawnSet;
    public AudioClip sfx;
    public float volume = 1f;

    private void Awake()
    {
        checkpoint = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        spawnSet = playerController.NewCheckpoint(checkpoint, checkpointNumber);

        if(spawnSet)
        {
            CharacterEvents.provideInfo.Invoke(gameObject, "Checkpoint Reached!");
            AudioSource.PlayClipAtPoint(sfx, gameObject.transform.position, volume);
        }

        
    }


}
