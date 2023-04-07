using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    Collider2D collider;
    public List<Collider2D> collidersDetected = new List<Collider2D>();

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidersDetected.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collidersDetected.Remove(collision);
    }
}
