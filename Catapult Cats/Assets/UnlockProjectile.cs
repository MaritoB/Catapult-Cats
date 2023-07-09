using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public string ProjectileName;
    public void Start()
    {
        if (PlayerPrefs.GetInt(ProjectileName) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt(ProjectileName, 1);
        PlayerPrefs.Save();
        gameObject.SetActive(false);

    }
}
