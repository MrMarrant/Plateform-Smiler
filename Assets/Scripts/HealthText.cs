using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 100, 0);
    public float timeToFade = 3f;
    private float timeElapsed = 0f;
    private Color startColor;

    RectTransform rectTransform;
    TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        startColor = textMeshProUGUI.color;
    }

    // Update is called once per frame
    private void Update()
    {
        rectTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;

        if (timeElapsed < timeToFade)
        {
            float newAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshProUGUI.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
        }
        else Destroy(gameObject);
    }
}
