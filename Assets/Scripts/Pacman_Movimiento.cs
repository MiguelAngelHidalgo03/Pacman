using System.Collections;
using UnityEngine;

public class Movimiento_Pacman : MonoBehaviour
{
    public float velocidad = 5.0f;
    public float tiempoEntreSprites = 0.2f; // Tiempo entre cambios de sprite

    // Sprites normales para las 4 direcciones
    public Sprite[] spritesNormalArriba;
    public Sprite[] spritesNormalAbajo;
    public Sprite[] spritesNormalIzquierda;
    public Sprite[] spritesNormalDerecha;

    // Sprites para cuando está comiendo fantasmas
    public Sprite[] spritesComiendoArriba;
    public Sprite[] spritesComiendoAbajo;
    public Sprite[] spritesComiendoIzquierda;
    public Sprite[] spritesComiendoDerecha;

    public Sprite[] spritesMuerte; // Sprites de muerte
    public AudioClip sonidoMovimiento;
    public AudioClip sonidoMuerte; // Sonido de muerte
    public AudioClip musicaComiendo; // Música cuando está comiendo fantasmas

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private Vector2 direccion;
    private int indiceSprite = 0;
    private float temporizadorCambioSprite;

    private bool estaMuerto = false;
    private bool comiendoFantasmas = false;
    public bool invencible = false; // Controla si Pac-Man es invencible

    private GestorMusica gestorMusica;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = spritesNormalDerecha[0]; // Inicialmente mirando hacia la derecha

        gestorMusica = FindObjectOfType<GestorMusica>();
    }

    void Update()
    {
        if (estaMuerto)
        {
            return; // No hacer nada si está muerto
        }

        // Detectar entrada del usuario y establecer la dirección
        if (Input.GetKey(KeyCode.UpArrow))
        {
            direccion = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direccion = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direccion = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direccion = Vector2.right;
        }
        else
        {
            direccion = Vector2.zero;
        }

        // Reproducir sonido si se mueve
        if (direccion != Vector2.zero && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sonidoMovimiento);
        }
    }

    void FixedUpdate()
    {
        if (estaMuerto)
        {
            return; // No hacer nada si está muerto
        }

        // Mover el Pacman usando Rigidbody2D
        if (direccion != Vector2.zero)
        {
            Vector2 nuevaPosicion = rb2d.position + direccion * velocidad * Time.fixedDeltaTime;
            rb2d.MovePosition(nuevaPosicion);
            AnimarMovimiento();
        }
    }

    void AnimarMovimiento()
    {
        temporizadorCambioSprite += Time.deltaTime;
        if (temporizadorCambioSprite >= tiempoEntreSprites)
        {
            if (comiendoFantasmas)
            {
                // Animar el sprite de comer fantasmas
                if (direccion == Vector2.up)
                {
                    spriteRenderer.sprite = ObtenerSpriteDeArray(spritesComiendoArriba);
                }
                else if (direccion == Vector2.down)
                {
                    spriteRenderer.sprite = ObtenerSpriteDeArray(spritesComiendoAbajo);
                }
                else if (direccion == Vector2.left)
                {
                    spriteRenderer.sprite = ObtenerSpriteDeArray(spritesComiendoIzquierda);
                }
                else if (direccion == Vector2.right)
                {
                    spriteRenderer.sprite = ObtenerSpriteDeArray(spritesComiendoDerecha);
                }
            }
            else
            {
                // Animar el sprite normal
                if (direccion == Vector2.up)
                {
                    spriteRenderer.sprite = ObtenerSpriteDeArray(spritesNormalArriba);
                }
                else if (direccion == Vector2.down)
                {
                    spriteRenderer.sprite = ObtenerSpriteDeArray(spritesNormalAbajo);
                }
                else if (direccion == Vector2.left)
                {
                    spriteRenderer.sprite = ObtenerSpriteDeArray(spritesNormalIzquierda);
                }
                else if (direccion == Vector2.right)
                {
                    spriteRenderer.sprite = ObtenerSpriteDeArray(spritesNormalDerecha);
                }
            }

            temporizadorCambioSprite = 0;
        }
    }

    Sprite ObtenerSpriteDeArray(Sprite[] arraySprites)
    {
        indiceSprite = (indiceSprite + 1) % arraySprites.Length;
        return arraySprites[indiceSprite];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fantasma"))
        {
            if (comiendoFantasmas)
            {
                // Lógica para comer el fantasma
                Movimiento_Fantasmas fantasma = other.GetComponent<Movimiento_Fantasmas>();
                if (fantasma != null)
                {
                    fantasma.Desaparecer();
                    // Añadir puntos aquí si es necesario
                    Puntuacion.AnadirPuntos(200);
                    gestorMusica.ComerFantasma();
                }
            }
            else if (!invencible)
            {
                // Lógica para morir si no está comiendo fantasmas y no es invencible
                if (!estaMuerto)
                {
                    StartCoroutine(Morir());
                }
            }
        }
        else if (other.CompareTag("Frutas"))
        {
            // Lógica para tomar la fruta
            // Destruir la fruta, sumar puntos, etc.
            ActivarInvencibilidad(10.0f);
            gestorMusica.ComerFruta(10.0f);
            Destroy(other.gameObject);
        }
    }

    IEnumerator Morir()
    {
        estaMuerto = true;
        audioSource.PlayOneShot(sonidoMuerte);

        foreach (var sprite in spritesMuerte)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(tiempoEntreSprites); // Cambiar sprite cada tiempoEntreSprites segundos
        }

        // Aquí puedes implementar lógica adicional después de la animación de muerte
        // Por ejemplo, reiniciar el nivel, restar vidas, etc.
    }

    public void IniciarModoComiendoFantasma(float duracion)
    {
        StartCoroutine(ModoComiendoFantasma(duracion));
    }

    IEnumerator ModoComiendoFantasma(float duracion)
    {
        comiendoFantasmas = true;
        invencible = true; // Hacer que Pac-Man sea invencible
        AudioClip musicaOriginal = audioSource.clip;
        audioSource.clip = musicaComiendo;
        audioSource.Play();

        yield return new WaitForSeconds(duracion);

        comiendoFantasmas = false;
        invencible = false; // Desactivar invencibilidad
        audioSource.clip = musicaOriginal;
        audioSource.Play();
    }

    public void ActivarInvencibilidad(float duracion)
    {
        StartCoroutine(ModoInvencible(duracion));
    }

    IEnumerator ModoInvencible(float duracion)
    {
        invencible = true; // Hacer que Pac-Man sea invencible

        yield return new WaitForSeconds(duracion);

        invencible = false; // Desactivar invencibilidad
    }
}
