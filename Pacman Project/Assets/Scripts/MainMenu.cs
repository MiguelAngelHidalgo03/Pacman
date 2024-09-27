using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escenas
using UnityEngine.UI; // Para los componentes UI

public class MainMenu : MonoBehaviour
{
    // Este m�todo se enlaza al bot�n "Play"
    public void PlayGame()
    {
        // Aqu� debes poner el nombre de la escena de tu juego, por ejemplo, "PacmanLevel1"
        SceneManager.LoadScene("PacmanLevel1");
    }

    // Este m�todo se enlaza al bot�n "Score"
    public void ViewScore()
    {
        // Aqu� puedes cargar una escena de puntuaci�n, o mostrar un panel de puntuaci�n
        Debug.Log("Mostrando la puntuaci�n...");
        // Si tienes una escena de puntuaci�n, usar�as algo como:
        // SceneManager.LoadScene("ScoreScene");
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Esto solo funciona en la aplicaci�n compilada, no en el editor de Unity
    }
}
