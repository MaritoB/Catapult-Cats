using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField]
    string SceneName;
    [SerializeField]
    int LevelNumber;
    LevelSelectManager Manager;
    // Start is called before the first frame update
    void Start()
    {
        Manager = LevelSelectManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel()
    {
            Manager.LoadLevel(SceneName);
    }


}
