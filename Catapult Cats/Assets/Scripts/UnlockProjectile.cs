using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public int  ProjectileIndex, Amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.AddAmmoToProjectile(ProjectileIndex, Amount, GetComponentInChildren<SpriteRenderer>().sprite);
        //Sound and Particles
        gameObject.SetActive(false);

    }
}
