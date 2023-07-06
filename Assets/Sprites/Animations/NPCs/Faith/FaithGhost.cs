using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithGhost : MonoBehaviour
{
    public float flickerSpeed = 0.5f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * flickerSpeed, 1.0f) * 0.5f; // Now alpha ranges between 0 and 0.5.
        spriteRenderer.color = new Color(0f, 0f, 0f, alpha);
    }
}