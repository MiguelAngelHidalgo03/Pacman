using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destino; // La posición a la que queremos teletransportar
    public float cooldown = 1.0f; // Tiempo de espera entre usos de portales

    private float tiempoUltimoUso;
    private bool enUso;

    private void Start()
    {
        tiempoUltimoUso = -cooldown; // Iniciar el temporizador
        enUso = false; // Estado inicial del portal
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time >= tiempoUltimoUso + cooldown)
        {
            // Marcar el portal como en uso
            if (!enUso)
            {
                enUso = true;
                other.transform.position = destino.position;
                tiempoUltimoUso = Time.time;
                Invoke("ResetPortalUsage", cooldown); // Restablecer el estado después del cooldown
            }
        }
    }

    private void ResetPortalUsage()
    {
        enUso = false;
    }
}
