using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parpadeo : MonoBehaviour
{
    public float duracionParpadeo = 0.01f;

    private SpriteRenderer spriteRenderer;
    private bool visible = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("Parpadear", duracionParpadeo, duracionParpadeo);
    }

    private void Parpadear()
    {
        visible = !visible;
        spriteRenderer.enabled = visible;
    }
}
