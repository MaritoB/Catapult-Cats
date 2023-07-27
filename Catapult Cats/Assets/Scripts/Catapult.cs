using System.Collections;
using UnityEngine;
using FMODUnity;

public class Catapult : MonoBehaviour
{
    public GameObject aim;
    public Transform spawnPoint;
    public Vector3 CameraTargetPointOffset;
    [SerializeField]
    private Animator CatAnimator;

    [SerializeField]
    private EventReference CatapultLaunchSound, CatWinSound, CatLoseSound;
    [SerializeField]
    private int Shoots, initialSmallStoneAmmo;

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
        LoadSaves();
        if (CurrentProjectile.GetAmmo() == 0)
        {
            SelectNextProjectile();
        }
        gameManager.gameUI.UpdateShoots(CurrentProjectile.GetAmmo());
        gameManager.gameUI.ChangeProjectileImage(CurrentProjectile.body.GetComponent<SpriteRenderer>().sprite);
        LoadSaves();
    }
    public void CatAimAnimation()
    {
        CatAnimator.SetTrigger("Aim");
    }
    public void CatWinAnimation()
    {
        AudioManager.instance.PlayOneShot(CatWinSound, this.transform.position);
        CatAnimator.SetTrigger("Win");
    }
    public void CatLoseAnimation()
    {
        AudioManager.instance.PlayOneShot(CatLoseSound, this.transform.position);
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
    public void AddAmmoToProjectile(int aProjectileIndex, int aAmount )
    {
        Projectiles[aProjectileIndex].AddAmmo(aAmount);
    }
    public void LoadSaves()
    {
            Projectiles[0].SetAmmo(PlayerPrefs.GetInt("SmallStone"));
            Projectiles[1].SetAmmo(PlayerPrefs.GetInt("BigStone"));
            Projectiles[2].SetAmmo(PlayerPrefs.GetInt("MultipleProjectiles"));
            Projectiles[3].SetAmmo(PlayerPrefs.GetInt("Blade"));
            Projectiles[4].SetAmmo(PlayerPrefs.GetInt("Fireball"));
    }
    public void Save()
    {
        PlayerPrefs.SetInt("SmallStone", Projectiles[0].GetAmmo());
        PlayerPrefs.SetInt("BigStone", Projectiles[1].GetAmmo());
        PlayerPrefs.SetInt("MultipleProjectiles", Projectiles[2].GetAmmo());
        PlayerPrefs.SetInt("Blade", Projectiles[3].GetAmmo());
        PlayerPrefs.SetInt("Fireball", Projectiles[4].GetAmmo());
    }
    public void ResetProgress()
    {
        PlayerPrefs.SetInt("SmallStone", initialSmallStoneAmmo);
        PlayerPrefs.SetInt("BigStone", 0);
        PlayerPrefs.SetInt("MultipleProjectiles", 0);
        PlayerPrefs.SetInt("Blade", 0);
        PlayerPrefs.SetInt("Fireball",0);

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
        if (Projectiles[ProjectileIndex].GetAmmo()<=0){
            SelectNextProjectile();
            return;
        }
        CurrentProjectile = Projectiles[ProjectileIndex];
        gameManager.gameUI.UpdateShoots(CurrentProjectile.GetAmmo());
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
        if (Projectiles[ProjectileIndex].GetAmmo() <= 0)
        {
            SelectPreviousProjectile();
            return;
        }
        CurrentProjectile = Projectiles[ProjectileIndex];
        gameManager.gameUI.UpdateShoots(CurrentProjectile.GetAmmo());
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
        if (CalculateShootsRemaining() <= 0)
        {
            // GAME OVER
            gameManager.ResetProgress();
            gameManager.EndInSeconds(0);
            return;
        }
        isFiring = true;
        if(CurrentProjectile != null)
        {
            if (CurrentProjectile.GetAmmo() == 0)
            {
                SelectNextProjectile();
            }
            gameManager.gameUI.UpdateShoots(CurrentProjectile.GetAmmo());
            CurrentProjectile.gameObject.SetActive(true);
            CurrentProjectile.SetProjectileToShoot(spawnPoint.position);
        }


    }
    public int CalculateShootsRemaining()
    {
        int sum = 0;
        for (int i = 0; i < Projectiles.Length; i++)
        {
            sum  += Projectiles[i].GetAmmo();
        }
        return sum;
    }
    public void CastProjectile(Vector2 aDirection, float aDragForcePercentage)
    {
        if (!canShoot || CurrentProjectile == null)
        {
            return;
        }
        CurrentProjectile.UseAmmo();
        gameManager.gameUI.UpdateShoots(CurrentProjectile.GetAmmo());
        AudioManager.instance.PlayOneShot(CatapultLaunchSound, this.transform.position);
        gameManager.gameUI.HideProjectileSelector();
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
        isFiring = false;
        Shoots--;
        gameManager.CameraController.TurnToProjectilCamera();
        gameManager.CameraController.SetFollowProjectile(CurrentProjectile.body.transform);
        CurrentProjectile.LaunchProyectile(spawnPoint.position, Direction * catapultForce * dragForcePercentage * CurrentProjectile.rb.mass, gameManager.GetWind());
        StartCoroutine(ResetCanShootInSeconds(7f));

    }
}