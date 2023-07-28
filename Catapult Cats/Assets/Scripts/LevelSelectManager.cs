using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance { get; private set; }
    public TMPro.TextMeshProUGUI TrophySumText;
    private Animator UIAnimator;
    bool isLoading = false;
    public RectTransform Player;
    int trophySum;
    // Start is called before the first frame update
    void Start()
    {
        UIAnimator = GetComponent<Animator>();
        StartCoroutine(onStartLate());
    }

    IEnumerator onStartLate()
    {
        yield return new WaitForSeconds(0.1f);
        trophySum = 0;
        int higherLevel = PlayerPrefs.GetInt("HigherLevelUnlocked");
        for (int i = 0; i < higherLevel; i++)
        {
            trophySum += PlayerPrefs.GetInt("ScoreLevel" + i);
        }
        TrophySumText.text = trophySum.ToString();
    }


        public void setPlayerPosition(Vector2 aPosition)
    {
        Player.position = aPosition + Vector2.up * 70;
    }

    // Update is called once per frame
    void Update()
    {

    }
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

    }
    public void LoadLevel(string aName)
    {
        if (isLoading == false)
        {
            isLoading = true; 
            UIAnimator.SetTrigger("FadeOut");
            StartCoroutine(LoadAsyncScene(aName));

        }
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
}

