using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escenas
using UnityEngine.UI; // Para los componentes UI

public class MainMenu : MonoBehaviour
{
    // Este método se enlaza al botón "Play"
    public void PlayGame()
    {
        // Aquí debes poner el nombre de la escena de tu juego, por ejemplo, "PacmanLevel1"
        SceneManager.LoadScene("PacmanLevel1");
    }

    // Este método se enlaza al botón "Score"
    public void ViewScore()
    {
        // Aquí puedes cargar una escena de puntuación, o mostrar un panel de puntuación
        Debug.Log("Mostrando la puntuación...");
        // Si tienes una escena de puntuación, usarías algo como:
        // SceneManager.LoadScene("ScoreScene");
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Esto solo funciona en la aplicación compilada, no en el editor de Unity
    }
}
