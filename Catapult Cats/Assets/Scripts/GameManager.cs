using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int totalEnemies = 0; // Número total de enemigos en la escena
    private int killedEnemies; // Número de enemigos eliminados
    private int projectileCount = 0;
    public CameraController CameraController;
    [SerializeField]
    private bool levelCompleted; // Indica si se ha completado el nivel
    private int starRating; // Puntuación en estrellas
    [SerializeField]
    private GameObject UIEndGame;
    private Animator UIEndGamePanelAnimator;
    [SerializeField]
    CloudsMovement Clouds;
    [SerializeField]
    private Vector2 WindForceRange;
    [SerializeField]
    private Vector2 WindForce;
    [SerializeField]
    private ParticleSystem WindPS;
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
        WindForce.x = Random.Range(WindForceRange.x, WindForceRange.y);
        Clouds.SetWindForce(WindForce.x);
        if(WindPS != null)
        {
            ParticleSystem.ForceOverLifetimeModule WindForceModule = WindPS.forceOverLifetime;
            WindForceModule.x = WindForce.x;
            WindForceModule.y = WindForce.y;
        }
        //DontDestroyOnLoad(gameObject);
    }
    public void AddEnemyCount()
    {
        totalEnemies++;
    }

    void Start()
    {
        Application.targetFrameRate = 60; // Establece el FPS máximo en 60
        InitializeLevel();
        UIEndGamePanelAnimator = UIEndGame.GetComponent<Animator>();
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
        if (projectileCount >= 3)
        {
            StartCoroutine(EndInSeconds(8f));
        }
    }

    IEnumerator EndInSeconds(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);
        GameOver();
    }

    public void GameOver()
    {

        if (!levelCompleted)
        {
            levelCompleted = true;
            CalculateStarRating();
            ShowLevelResult();
        }
    }
    public void EnemyKilled()
    {
        killedEnemies++;

        if (killedEnemies == totalEnemies)
        {
            StartCoroutine(EndInSeconds(1f));
        }
    }

    private void CalculateStarRating()
    {
        if (killedEnemies < totalEnemies)
        {
            starRating = 0;
            return;
        }
        else
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
    }
    public Vector2 GetWind() {
        return WindForce;
    }
    public void ReloadLevel()
    {
        UIEndGamePanelAnimator.SetTrigger("FadeOut");
        StartCoroutine(LoadAsyncScene(SceneManager.GetActiveScene().name));
        
    }
    IEnumerator LoadAsyncScene(string aScene)
    {
        //wait for fadeOut
        yield return new WaitForSeconds(1.9f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(aScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    private void ShowLevelResult()
    {
        UIEndGame.SetActive(true);
        UIEndGamePanelAnimator.SetTrigger("" + starRating);

    }
}
