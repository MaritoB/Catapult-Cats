using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject aim;
    public Transform spawnPoint;
    public Vector3 CameraTargetPointOffset;
    
    private GameManager gameManager;
    public float catapultForce;
    private float dragForcePercentage;

    public Projectile[] Projectiles;
    int ProjectileIndex = 0;
    private  Projectile CurrentProjectile;
    private Animator animator;
    private bool canShoot = true;

    public bool CanShoot
    {
        get { return canShoot; }
        private set { canShoot = value; }
    }
    private Vector2 Direction = Vector2.zero;
    public bool isFiring = false;

    void Start()
    {
        PlayerPrefs.SetInt("SmallStone", 1);
        PlayerPrefs.Save();
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
        CurrentProjectile = Projectiles[0];
        gameManager.gameUI.ChangeProjectileImage(CurrentProjectile.body.GetComponent<SpriteRenderer>().sprite);
        LoadSaves();
    }
    public void UnlockProjectilWithIndex(int aIndex)
    {
        Projectiles[aIndex].UnlockProjectile();
    }
    public void LoadSaves()
    {
        if (PlayerPrefs.GetInt("SmallStone") == 1)
        {
            Projectiles[0].UnlockProjectile();
        }
        if (PlayerPrefs.GetInt("MultipleProjectiles") == 1)
        {
            Projectiles[1].UnlockProjectile();
        }
        if (PlayerPrefs.GetInt("BigStone") == 1)
        {
            Projectiles[2].UnlockProjectile();
        }
        if (PlayerPrefs.GetInt("Blade") == 1)
        {
            Projectiles[3].UnlockProjectile();
        }
        if (PlayerPrefs.GetInt("Fireball") == 1)
        {
            Projectiles[4].UnlockProjectile();
        }
    }
    public void SelectNextProjectile()
    {
        Projectiles[ProjectileIndex].TurnOffProjectile();
        ProjectileIndex++;
        if(ProjectileIndex >= Projectiles.Length)
        {
            ProjectileIndex = 0;
        }
        if (!Projectiles[ProjectileIndex].IsUnlocked()){
            SelectNextProjectile();
            return;
        }
        CurrentProjectile = Projectiles[ProjectileIndex];
        gameManager.gameUI.ChangeProjectileImage(CurrentProjectile.body.GetComponent<SpriteRenderer>().sprite);
        setupProjectile();
    }
    public void SelectPreviousProjectile()
    {
        Projectiles[ProjectileIndex].TurnOffProjectile();
        ProjectileIndex--;
        if (ProjectileIndex <0)
        {
            ProjectileIndex = Projectiles.Length-1;
        }
        if (!Projectiles[ProjectileIndex].IsUnlocked())
        {
            SelectPreviousProjectile();
            return;
        }
        CurrentProjectile = Projectiles[ProjectileIndex];
        gameManager.gameUI.ChangeProjectileImage(CurrentProjectile.body.GetComponent<SpriteRenderer>().sprite);
        setupProjectile();
    }
    void LateUpdate()
    {
        if (isFiring)
        {
            CurrentProjectile.body.transform.position = spawnPoint.position;
        }
    }
    public void setupProjectile()
    {
        isFiring = true;
        if(CurrentProjectile != null)
        {
            CurrentProjectile.gameObject.SetActive(true);
            CurrentProjectile.SetProjectileToShoot(spawnPoint.position);
        }

    }
    public void CastProjectile(Vector2 aDirection, float aDragForcePercentage)
    {
        if (!canShoot || CurrentProjectile == null)
        {
            return;
        }
        gameManager.ShootProjectile();

        Direction = aDirection;
        dragForcePercentage = aDragForcePercentage;
        canShoot = false;
        animator.SetTrigger("Launch");
    }
    IEnumerator ResetCanShoot()
    {
        yield return new WaitForSeconds(7f);
        gameManager.CameraController.TurnToCatapultCamera();
        gameManager.gameUI.ShowProjectileSelector();
        canShoot = true;
        setupProjectile();

    }
    public void LaunchProyectil()
    {
        Debug.Log("Launch");
        isFiring = false;
        gameManager.gameUI.HideProjectileSelector();
        gameManager.CameraController.TurnToProjectilCamera();
        gameManager.CameraController.SetFollowProjectile(CurrentProjectile.body);
        CurrentProjectile.LaunchProyectile(spawnPoint.position, Direction * catapultForce * dragForcePercentage * CurrentProjectile.rb.mass, gameManager.GetWind());
        StartCoroutine(ResetCanShoot());

    }
}