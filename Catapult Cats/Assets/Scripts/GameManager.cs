using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int totalEnemies = 0; // Número total de enemigos en la escena
    private int killedEnemies; // Número de enemigos eliminados
    private int projectileCount = 0;

    private bool levelCompleted; // Indica si se ha completado el nivel
    private int starRating; // Puntuación en estrellas

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    public void AddEnemyCount()
    {
        totalEnemies++;
    }

    void Start()
    {
        Application.targetFrameRate = 60; // Establece el FPS máximo en 60
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        killedEnemies = 0;
        levelCompleted = false;
        starRating = 0;
        totalEnemies = 0;
        projectileCount = 0;
    }
    public  void ShootProjectile()
    {
        projectileCount++;
        if(projectileCount >= 3)
        {
            Debug.Log("Fin de la partida");
            CalculateStarRating();
            ShowLevelResult();
        }
    }

    public void EnemyKilled()
    {
        killedEnemies++;

        if (killedEnemies >= totalEnemies)
        {
            levelCompleted = true;
            CalculateStarRating();
            ShowLevelResult();
        }
    }

    private void CalculateStarRating()
    {
        if (projectileCount == 1)
        {
            starRating = 3;
        }
        else if (projectileCount == 2)
        {
            starRating = 2;
        }
        else if (projectileCount == 3)
        {
            starRating = 1;
        }
    }

    private void ShowLevelResult()
    {
        // Lógica para mostrar el resultado del nivel, como una pantalla de victoria con las estrellas obtenidas
        Debug.Log("Level Completed!");
        Debug.Log("Star Rating: " + starRating);
    }
}
