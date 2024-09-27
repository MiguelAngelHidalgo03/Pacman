using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntuacion : MonoBehaviour
{
    public static int puntuacionTotal = 0; // Puntaje total
    public Text textoPuntuacion; // Referencia al componente de texto para mostrar el puntaje

    private void Start()
    {
        // Inicializar el texto de la puntuación
        ActualizarTextoPuntuacion();
    }

    private void Update()
    {
        // Actualizar el texto del puntaje en la UI
        ActualizarTextoPuntuacion();
    }

    public static void AnadirPuntos(int puntos)
    {
        puntuacionTotal += puntos;
    }

    private void ActualizarTextoPuntuacion()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = puntuacionTotal.ToString();
        }
    }
}
