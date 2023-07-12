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
    [SerializeField]
    TMPro.TextMeshProUGUI LevelNumberText;
    LevelSelectManager Manager;
    bool isUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        Manager = LevelSelectManager.Instance;
        int higherLevelUnlocked = PlayerPrefs.GetInt("HigherLevelUnlocked");
        isUnlocked = higherLevelUnlocked  >= levelNumber;
        if(higherLevelUnlocked == levelNumber)
        {
            Manager.setPlayerPosition(transform.position);
        }
        if (!isUnlocked)
        {
            gameObject.SetActive(false);
        }
        int trophyNumber = PlayerPrefs.GetInt("ScoreLevel" + levelNumber);
        for (int i = 0; i < 3; i++)
        {
            Trophys[i].SetActive( i < trophyNumber );
        }
        LevelNumberText.text = levelNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel()
    {
            AudioManager.instance.PlayOneShot(buttonPressedSound, this.transform.position);
            Manager.LoadLevel(SceneName);
    }


}
