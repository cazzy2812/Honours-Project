using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlayer : MonoBehaviour
{
    private Collider2D collider;
    private bool playerOnPlatform;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objectCollided = collision.gameObject;

        if (objectCollided.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject objectCollided = collision.gameObject;

        if (objectCollided.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    private void Update()
    {
        if(playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1f);
        collider.enabled = true;
    }
}
