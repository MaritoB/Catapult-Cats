using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    Image CurrentProjectileImage;
    [SerializeField]
    Image AddProjectileImage;
    Animator animator;
    [SerializeField]
    TMPro.TextMeshProUGUI ShootsRemainingText, AddProjectileText;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }
    public void AddProjectile(Sprite aSprite, int aNumber)
    {
        AddProjectileImage.sprite = aSprite;
        AddProjectileText.text = "+" + aNumber.ToString();
        animator.SetTrigger("AddProjectile");

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
    public void UpdateShoots(int aNumber)
    {
        ShootsRemainingText.text = aNumber.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
