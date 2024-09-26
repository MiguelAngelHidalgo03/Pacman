using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int puntos = 10; // Puntos otorgados por la moneda

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Añadir puntos al puntaje total
            Puntuacion.puntuacionTotal += puntos;

            // Destruir la moneda
            Destroy(gameObject);
        }
    }
}

