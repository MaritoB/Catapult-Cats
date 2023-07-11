using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField]
    private EventReference buttonPressedSound;
    [SerializeField]
    string SceneName;
    [SerializeField]
    int levelNumber;
    [SerializeField]
    GameObject[] Trophys;
    LevelSelectManager Manager;
    bool isUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        Manager = LevelSelectManager.Instance;
        isUnlocked = PlayerPrefs.GetInt("HigherLevelUnlocked") >= levelNumber;
        if (!isUnlocked)
        {
            gameObject.SetActive(false);
        }
        PlayerPrefs.GetInt("ScoreLevel" + levelNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel()
    {
            Manager.LoadLevel(SceneName);
            AudioManager.instance.PlayOneShot(buttonPressedSound, this.transform.position);
    }


}
