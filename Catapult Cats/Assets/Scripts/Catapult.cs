using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject projectileGameObject;
    public GameObject aim;
    public Transform spawnPoint;
    public Transform ProjectilCamera;
    public Vector3 CameraTargetPointOffset;

    private GameManager gameManager;
    public float catapultForce;
    private float dragForcePercentage;

    public Projectile projectile;
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
        projectile = projectileGameObject.GetComponent<Projectile>();

    }
    void Update()
    {
        
        if (isFiring)
        {
            projectile.body.transform.position = spawnPoint.position;
        }


        
    }
    public void setupProjectile()
    {
        isFiring = true;
        if(projectileGameObject != null && projectile != null)
        {
            projectile.gameObject.SetActive(true);
            projectile.SetProjectileToShoot(spawnPoint.position);
        }

    }
    public void CastProjectile(Vector2 aDirection, float aDragForcePercentage)
    {
        if (!canShoot || projectile == null)
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
        canShoot = true;
        setupProjectile();

    }
    public void LaunchProyectil()
    {
        Debug.Log("Launch");
        isFiring = false;
        gameManager.CameraController.TurnToProjectilCamera();
        projectile.LaunchProyectile(spawnPoint.position, Direction * catapultForce * dragForcePercentage, gameManager.GetWind());
        StartCoroutine(ResetCanShoot());

    }
}