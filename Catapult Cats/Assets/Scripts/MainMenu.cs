using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public int initialSmallStoneAmmo;
    Animator animator;
    [SerializeField]
    private EventReference buttonPressedSound, MainMenuMusic;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(onStartLate());
    }
    IEnumerator onStartLate()
    {
        yield return new WaitForSeconds(0.1f);
        AudioManager.instance.PlayOneShot(MainMenuMusic, this.transform.position);

    }

     public void PlayNewGame()
    {
        ResetProgress();
        AudioManager.instance.PlayOneShot(buttonPressedSound, this.transform.position);
        animator.SetTrigger("FadeOut");
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
    void ResetProgress()
    {
        int higherLevel = PlayerPrefs.GetInt("HigherLevelUnlocked");
        for (int i = 0; i < higherLevel; i++)
        {

            PlayerPrefs.SetInt("ScoreLevel" + i, 0);
        }
        PlayerPrefs.SetInt("HigherLevelUnlocked", 1);
        PlayerPrefs.SetInt("SmallStone", initialSmallStoneAmmo);
        PlayerPrefs.SetInt("BigStone", 0);
        PlayerPrefs.SetInt("MultipleProjectiles", 0);
        PlayerPrefs.SetInt("Blade", 0);
        PlayerPrefs.SetInt("Fireball", 0);
        PlayerPrefs.Save();
    }
}
