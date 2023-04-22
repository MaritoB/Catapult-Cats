
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button startButton; // Botón de inicio

    private void Start()
    {
        startButton.onClick.AddListener(StartGame); // Agregar función de inicio al botón
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Scene1"); // Cargar la escena del juego
    }
}

