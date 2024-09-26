using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frutas : MonoBehaviour
{
    public int puntos = 200;
    public float duracionEfecto = 7.0f; // Duración del efecto de la fruta
    public Sprite[] spritesFruta; // Sprites para la fruta
    public AudioClip sonidoFruta;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool efectoActivo = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Añadir puntos al puntaje total
            Puntuacion.AnadirPuntos(puntos);

            // Reproducir sonido
            if (sonidoFruta != null)
            {
                audioSource.PlayOneShot(sonidoFruta);
            }

            // Iniciar el efecto de la fruta
            if (!efectoActivo)
            {
                StartCoroutine(ActivarEfecto());
            }

            // Destruir la fruta
            Destroy(gameObject);
        }
    }

    private IEnumerator ActivarEfecto()
    {
        efectoActivo = true;
        Debug.Log("Efecto de fruta activado");

        // Cambiar sprites de los fantasmas y Pac-Man y cambiar la música
        ControladorJuego.IniciarModoFantasmasComestibles(duracionEfecto);

        // Cambiar sprite de la fruta para mostrar que está activa
        for (int i = 0; i < spritesFruta.Length; i++)
        {
            spriteRenderer.sprite = spritesFruta[i];
            yield return new WaitForSeconds(0.1f); // Tiempo entre cambios de sprite
        }

        efectoActivo = false;
        Debug.Log("Efecto de fruta desactivado");
    }
}
