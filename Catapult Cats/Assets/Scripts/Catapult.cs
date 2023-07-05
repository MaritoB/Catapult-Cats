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
        gameManager = GameManager.Instance;
        animator = GetComponent<Animator>();
        CurrentProjectile = Projectiles[0];
    }
    public void SelectNextProjectile()
    {
        Projectiles[ProjectileIndex].TurnOffProjectile();
        ProjectileIndex++;
        if(ProjectileIndex >= Projectiles.Length)
        {
            ProjectileIndex = 0;
        }
        CurrentProjectile = Projectiles[ProjectileIndex];
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
        CurrentProjectile = Projectiles[ProjectileIndex];
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
        canShoot = true;
        setupProjectile();

    }
    public void LaunchProyectil()
    {
        Debug.Log("Launch");
        isFiring = false;
        gameManager.CameraController.TurnToProjectilCamera();
        gameManager.CameraController.SetFollowProjectile(CurrentProjectile.body);
        CurrentProjectile.LaunchProyectile(spawnPoint.position, Direction * catapultForce * dragForcePercentage * CurrentProjectile.rb.mass, gameManager.GetWind());
        StartCoroutine(ResetCanShoot());

    }
}