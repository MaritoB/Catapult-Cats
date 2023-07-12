using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject aim;
    public Transform spawnPoint;
    public Vector3 CameraTargetPointOffset;
    [SerializeField]
    private Animator CatAnimator;

    [SerializeField]
    private int Shoots;

    [SerializeField]
    private GameManager gameManager;
    public float catapultForce;
    private float dragForcePercentage;

    public Projectile[] Projectiles;
    int ProjectileIndex = 0;
    private  Projectile CurrentProjectile;
    private Animator animator;
    [SerializeField]
    private bool canShoot = true;

    private Vector2 Direction = Vector2.zero;
    public bool isFiring = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        CurrentProjectile = Projectiles[0];
        StartCoroutine(onStartLate());
    }

    public bool CanShoot()
    {
        return canShoot;
    }
    public void setCanShoot(bool aBool)
    {
        this.canShoot = aBool;
    }
    IEnumerator onStartLate()
    {
        yield return new WaitForSeconds(0.1f);
        gameManager = GameManager.Instance;
        gameManager.SetMaxShoots(Shoots);
        gameManager.gameUI.UpdateShoots(Shoots);
        gameManager.gameUI.ChangeProjectileImage(CurrentProjectile.body.GetComponent<SpriteRenderer>().sprite);
        LoadSaves();
    }
    public void CatAimAnimation()
    {
        CatAnimator.SetTrigger("Aim");
    }
    public void CatWinAnimation()
    {
        CatAnimator.SetTrigger("Win");
    }
    public void CatLoseAnimation()
    {
        CatAnimator.SetTrigger("Lose");
    }
    public void CatLaunchAnimation()
    {
        CatAnimator.SetTrigger("Launch");
    }
    public void CatIdleAnimation()
    {
        CatAnimator.SetTrigger("Idle");
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
        if (PlayerPrefs.GetInt("BigStone") == 1)
        {
            Projectiles[1].UnlockProjectile();
        }
        if (PlayerPrefs.GetInt("MultipleProjectiles") == 1)
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
    public void ResetShoot()
    {
        StartCoroutine(ResetCanShootInSeconds(0f));
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
        if (Shoots<=0)
        {
            StartCoroutine(gameManager.EndInSeconds(7f));
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
    IEnumerator ResetCanShootInSeconds(float aSeconds)
    {
        yield return new WaitForSeconds(aSeconds);
        if (Shoots> 0 && !canShoot)
        {
            gameManager.CameraController.TurnToCatapultCamera();
            gameManager.gameUI.ShowProjectileSelector();
            canShoot = true;
            setupProjectile();
        }
    }
    public void LaunchProyectil()
    {
        Debug.Log("Launch");
        isFiring = false;
        Shoots--;
        gameManager.gameUI.UpdateShoots(Shoots);
        gameManager.gameUI.HideProjectileSelector();
        gameManager.CameraController.TurnToProjectilCamera();
        gameManager.CameraController.SetFollowProjectile(CurrentProjectile.body);
        CurrentProjectile.LaunchProyectile(spawnPoint.position, Direction * catapultForce * dragForcePercentage * CurrentProjectile.rb.mass, gameManager.GetWind());
        StartCoroutine(ResetCanShootInSeconds(7f));

    }
}