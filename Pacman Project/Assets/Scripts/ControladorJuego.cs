using System.Collections;
using UnityEngine;

public class ControladorJuego : MonoBehaviour
{
    public static ControladorJuego instancia;

    public AudioClip musicaComiendoFantasma;
    private AudioClip musicaOriginal;
    public AudioSource audioSource;

    private Movimiento_Fantasmas[] fantasmas;
    private Movimiento_Pacman pacman;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Obtiene todas las referencias necesarias
        fantasmas = FindObjectsOfType<Movimiento_Fantasmas>();
        pacman = FindObjectOfType<Movimiento_Pacman>();
        musicaOriginal = audioSource.clip; // Guarda la m�sica original
    }

    public static void IniciarModoFantasmasComestibles(float duracion)
    {
        // Llama a la coroutine para cambiar el estado de los fantasmas
        instancia.StartCoroutine(instancia.CambiarModoFantasmas(duracion));
    }

    private IEnumerator CambiarModoFantasmas(float duracion)
    {
        Debug.Log("Inicio del modo comestible");

        // Cambia el estado de todos los fantasmas a comestibles
        foreach (var fantasma in fantasmas)
        {
            fantasma.HacerComestible(true);
        }

        // Cambia la m�sica
        audioSource.clip = musicaComiendoFantasma;
        audioSource.Play();

        yield return new WaitForSeconds(duracion); // Espera la duraci�n del efecto

        Debug.Log("Fin del modo comestible");

        // Revertir el estado de todos los fantasmas
        foreach (var fantasma in fantasmas)
        {
            fantasma.HacerComestible(false);
        }

        // Revertir la m�sica a la original
        audioSource.clip = musicaOriginal;
        audioSource.Play();
    }
}
