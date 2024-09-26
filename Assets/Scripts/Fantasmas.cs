using System.Collections;
using UnityEngine;

public class Movimiento_Fantasmas : MonoBehaviour
{
    public float velocidad = 3.0f;
    public Vector2 coordenadaReaparicion; // Coordenada donde reaparecerá el fantasma
    public float tiempoDesaparecer = 6.0f; // Tiempo en segundos para desaparecer

    public Sprite[] spritesArriba;
    public Sprite[] spritesAbajo;
    public Sprite[] spritesIzquierda;
    public Sprite[] spritesDerecha;

    public Sprite[] spritesComestibleArriba;
    public Sprite[] spritesComestibleAbajo;
    public Sprite[] spritesComestibleIzquierda;
    public Sprite[] spritesComestibleDerecha;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private Vector2 direccion;
    private float cambioDireccionTiempo = 3.0f;
    private float temporizador;
    private bool esComestible = false;
    private bool estaDesaparecido = false;

    private int indiceSprite = 0;
    private float tiempoCambioSprite = 0.2f; // Tiempo entre cambios de sprite
    private float temporizadorCambioSprite;
    private Sprite[] spritesActuales;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        CambiarDireccion();
    }

    void Update()
    {
        if (estaDesaparecido) return;

        temporizador += Time.deltaTime;

        if (temporizador >= cambioDireccionTiempo)
        {
            CambiarDireccion();
            temporizador = 0;
        }

        if (direccion != Vector2.zero)
        {
            AnimarMovimiento();
        }
    }

    void FixedUpdate()
    {
        if (estaDesaparecido) return;

        if (direccion != Vector2.zero)
        {
            Vector2 nuevaPosicion = rb2d.position + direccion * velocidad * Time.fixedDeltaTime;
            rb2d.MovePosition(nuevaPosicion);
        }
    }

    void AnimarMovimiento()
    {
        temporizadorCambioSprite += Time.deltaTime;
        if (temporizadorCambioSprite >= tiempoCambioSprite)
        {
            indiceSprite = (indiceSprite + 1) % spritesActuales.Length;
            spriteRenderer.sprite = spritesActuales[indiceSprite];
            temporizadorCambioSprite = 0;
        }
    }

    void CambiarDireccion()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0: // Arriba
                direccion = Vector2.up;
                spritesActuales = esComestible ? spritesComestibleArriba : spritesArriba;
                break;
            case 1: // Abajo
                direccion = Vector2.down;
                spritesActuales = esComestible ? spritesComestibleAbajo : spritesAbajo;
                break;
            case 2: // Izquierda
                direccion = Vector2.left;
                spritesActuales = esComestible ? spritesComestibleIzquierda : spritesIzquierda;
                break;
            case 3: // Derecha
                direccion = Vector2.right;
                spritesActuales = esComestible ? spritesComestibleDerecha : spritesDerecha;
                break;
        }
    }

    public void HacerComestible(bool comestible)
    {
        esComestible = comestible;
        CambiarDireccion(); // Cambiar a los sprites de la dirección actual
    }

    public void Desaparecer()
    {
        if (estaDesaparecido) return;

        StartCoroutine(DesaparecerTemporalmente());
    }

    private IEnumerator DesaparecerTemporalmente()
    {
        estaDesaparecido = true;
        spriteRenderer.enabled = false; // Ocultar el sprite
        rb2d.velocity = Vector2.zero; // Detener el movimiento

        yield return new WaitForSeconds(tiempoDesaparecer); // Esperar el tiempo de desaparición

        // Reaparecer en la coordenada deseada
        rb2d.position = coordenadaReaparicion;
        spriteRenderer.enabled = true; // Mostrar el sprite
        estaDesaparecido = false;
        CambiarDireccion(); // Reiniciar la dirección
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (esComestible)
            {
                // Lógica para comer el fantasma
                Desaparecer();
                // Asegúrate de añadir puntos aquí si es necesario
                Puntuacion.AnadirPuntos(100); // O la cantidad de puntos que desees
            }
            else
            {
                // Lógica para morir
                // Dependiendo del juego, aquí puede ir la lógica para morir
            }
        }
    }
}
