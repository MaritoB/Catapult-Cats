using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InGameUI gameUI;
    public int totalEnemies = 0; // Número total de enemigos en la escena
    private int killedEnemies; // Número de enemigos eliminados
    private int Shoots = 0;
    private int MaxShoots;
    [SerializeField]
    private EventReference buttonPressedSound; 
    [SerializeField]
    private int levelNumber;
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
        StartCoroutine(onStartLate());
    }
    IEnumerator onStartLate()
    {
        yield return new WaitForSeconds(0.1f);
        WindForce.x = Random.Range(WindForceRange.x, WindForceRange.y);
        Clouds.SetWindForce(WindForce.x);
        if (WindPS != null)
        {
            ParticleSystem.EmissionModule EmissionModule = WindPS.emission;
            ParticleSystem.ForceOverLifetimeModule WindForceModule = WindPS.forceOverLifetime;
            EmissionModule.rateOverTimeMultiplier = Mathf.Abs(WindForce.x) * 10;
            WindForceModule.x = WindForce.x;
            WindForceModule.y = WindForce.y;
        }
    }

    public void AddEnemyCount()
    {
        totalEnemies++;
    }
    public void SetMaxShoots(int aNumber)
    {
        MaxShoots = aNumber;
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
    }
    public  void ShootProjectile()
    {
        Shoots++;

    }

    public IEnumerator EndInSeconds(float Seconds)
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
            SaveProgress();
        }
    }
    void SaveProgress()
    {
        if (PlayerPrefs.GetInt("HigherLevelUnlocked") <= levelNumber)
        {
            if (PlayerPrefs.GetInt("ScoreLevel" + levelNumber) < starRating)
                        PlayerPrefs.SetInt("ScoreLevel" + levelNumber, starRating);
            PlayerPrefs.SetInt("HigherLevelUnlocked", levelNumber + 1);
            PlayerPrefs.Save();
        }
    }
    public void ResetProgress()
    {
        int higherLevel = PlayerPrefs.GetInt("HigherLevelUnlocked");
        for (int i = 0; i < higherLevel; i++)
        {

            PlayerPrefs.SetInt("ScoreLevel" + i, 0);
        }

        PlayerPrefs.SetInt("SmallStone", 1);
        PlayerPrefs.SetInt("MultipleProjectiles", 0);
        PlayerPrefs.SetInt("BigStone", 0);
        PlayerPrefs.SetInt("Fireball", 0);
        PlayerPrefs.SetInt("Blade", 0);
        PlayerPrefs.SetInt("HigherLevelUnlocked", 1);
        PlayerPrefs.Save();
        GoToLevelSelect();
    }
    public void EnemyKilled()
    {
        killedEnemies++;

        if (killedEnemies == totalEnemies)
        {
            StartCoroutine(EndInSeconds(2f));
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
            starRating = 1;
            float rating = (float)Shoots /(float) MaxShoots;
            Debug.Log("Number of Shoots: " + Shoots + " / " + MaxShoots + " = " + rating);
            if (rating <= (2f / 3f))
            {
                starRating = 2;
            }
            if (rating<=(1f / 3f))
            {
                starRating = 3;
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
    public void GoToLevelSelect()
    {
        AudioManager.instance.PlayOneShot(buttonPressedSound, this.transform.position);
        UIEndGamePanelAnimator.SetTrigger("FadeOut");
        StartCoroutine(LoadAsyncScene("LevelSelection"));

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
        gameUI.gameObject.SetActive(false); 
        UIEndGame.SetActive(true);
        UIEndGamePanelAnimator.SetTrigger("" + starRating);

    }
}
