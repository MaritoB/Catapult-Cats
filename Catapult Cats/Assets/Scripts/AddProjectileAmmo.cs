using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddProjectileAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    public int  ProjectileIndex, Amount;
    public bool isRandom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRandom)
        {
            GameManager.Instance.AddAmmoToRandomProjectile(Amount);

        }
        else
        {
             GameManager.Instance.AddAmmoToProjectile(ProjectileIndex, Amount, GetComponentInChildren<SpriteRenderer>().sprite);
        }
        //Sound and Particles
        gameObject.SetActive(false);

    }
}
