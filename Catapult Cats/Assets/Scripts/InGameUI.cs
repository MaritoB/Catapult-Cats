using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField]  
    Image CurrentProjectileImage;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }
    public void ChangeProjectileImage(Sprite aSprite)
    {
        CurrentProjectileImage.sprite = aSprite;
    }
    public void ShowProjectileSelector()
    {
        animator.SetTrigger("ShowProjectileSelector");
    }
    public void HideProjectileSelector()
    {
        animator.SetTrigger("HideProjectileSelector");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
