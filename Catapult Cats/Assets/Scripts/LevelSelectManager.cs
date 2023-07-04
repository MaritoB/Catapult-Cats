using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager Instance { get; private set; }
    private Animator UIAnimator; 
    bool isLoading;
    // Start is called before the first frame update
    void Start()
    {
        bool isLoading = false;
        UIAnimator = GetComponent<Animator>();
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

