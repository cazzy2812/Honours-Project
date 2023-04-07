using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPText : MonoBehaviour
{
    public Vector3 speed = new Vector3(0, 50, 0);
    public float disappearTime = 2f;
    private float timePassed = 0f;
    Color color;

    TextMeshProUGUI textMeshPro;

    RectTransform textTransform;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        color = textMeshPro.color;
    }

    private void Update()
    {
        textTransform.position += speed * Time.deltaTime;

        timePassed += Time.deltaTime;

        float alpha = color.a * (1 - (timePassed / disappearTime));

        if (timePassed < disappearTime )
        {
            textMeshPro.color = new Color(color.r, color.g, color.b, alpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
