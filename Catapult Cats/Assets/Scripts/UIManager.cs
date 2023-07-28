using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class UIManager : MonoBehaviour
{


    [SerializeField]
    private EventReference TrophySound;
    public void PlayTrophySound()
    {
        AudioManager.instance.PlayOneShot(TrophySound, Vector3.zero);
    }
}
