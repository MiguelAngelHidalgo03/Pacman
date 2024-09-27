using System.Collections;
using UnityEngine;

public class GestorMusica : MonoBehaviour
{
    public AudioClip musicaInicio; // Música que suena al inicio del juego
    public AudioClip musicaFondo; // Música de fondo que suena durante el juego
    public AudioClip musicaComiendoFantasmas; // Música cuando Pac-Man está en modo comer fantasmas
    public AudioClip sonidoComerFruta; // Sonido que suena al comer una fruta
    public AudioClip sonidoComerFantasma; // Sonido que suena al comer un fantasma

    private AudioSource audioSourceFondo; // AudioSource para la música de fondo
    private AudioSource audioSourceEfectos; // AudioSource para efectos de sonido

    private bool enModoComerFantasmas = false;

    void Start()
    {
        // Crear dos AudioSources, uno para la música de fondo y otro para efectos de sonido
        audioSourceFondo = gameObject.AddComponent<AudioSource>();
        audioSourceEfectos = gameObject.AddComponent<AudioSource>();

        // Iniciar con la música de inicio
        StartCoroutine(IniciarJuego());
    }

    IEnumerator IniciarJuego()
    {
        // Reproducir la música de inicio
        audioSourceFondo.clip = musicaInicio;
        audioSourceFondo.loop = false;
        audioSourceFondo.Play();

        // Esperar a que la música de inicio termine
        yield return new WaitForSeconds(musicaInicio.length);

        // Iniciar la música de fondo
        IniciarMusicaFondo();
    }

    void IniciarMusicaFondo()
    {
        // Reproducir la música de fondo
        audioSourceFondo.clip = musicaFondo;
        audioSourceFondo.loop = true;
        audioSourceFondo.Play();
    }

    public void ComerFruta(float duracionEfecto)
    {
        // Reproducir el sonido de comer fruta
        audioSourceEfectos.PlayOneShot(sonidoComerFruta);

        // Cambiar la música a la de comer fantasmas
        if (!enModoComerFantasmas)
        {
            StartCoroutine(MusicaModoComerFantasmas(duracionEfecto));
        }
    }

    IEnumerator MusicaModoComerFantasmas(float duracionEfecto)
    {
        enModoComerFantasmas = true;

        // Cambiar la música de fondo a la de comer fantasmas
        audioSourceFondo.clip = musicaComiendoFantasmas;
        audioSourceFondo.loop = true;
        audioSourceFondo.Play();

        // Esperar la duración del efecto
        yield return new WaitForSeconds(duracionEfecto);

        // Restaurar la música de fondo normal
        enModoComerFantasmas = false;
        IniciarMusicaFondo();
    }

    public void ComerFantasma()
    {
        // Reproducir el sonido de comer un fantasma sin interrumpir la música de fondo
        audioSourceEfectos.PlayOneShot(sonidoComerFantasma);
    }
}
