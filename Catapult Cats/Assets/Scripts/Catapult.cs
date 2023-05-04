using UnityEngine;

public class Catapult : MonoBehaviour
{
    public Projectile projectile;
    public GameObject aim;
    public Transform spawnPoint;

    public float baseForceMagnitude;

    private Animator animator;
    private bool canShoot = true;
    private Vector2 Direction = Vector2.zero;
    private float ForceMultiplier;

    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = projectile.GetComponent<Rigidbody2D>();
    }
    public void setProjectileToShoot()
    {
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        projectile.gameObject.SetActive(true);
        projectile.ps.gameObject.SetActive(true);
        projectile.transform.position = spawnPoint.position;
    }

    void Update()
    {
        if (!canShoot)
        {
            projectile.transform.position = spawnPoint.position;
            return;
        }
    }
    public void CastProjectile(Vector2 aDirection, float aForceMultiplier)
    {
        Direction = aDirection;
        ForceMultiplier = aForceMultiplier;
        Debug.Log(Direction.ToString() + ", " + ForceMultiplier);
        canShoot = false;
        animator.SetTrigger("Launch");
    }
    public void LaunchProyectil()
    {
        projectile.onCatapult = false;
        projectile.ResetLifeTime();
        canShoot = true;
        rb.gravityScale = 1;
        rb.angularVelocity = -500f;
        projectile.transform.position = spawnPoint.position;
        rb.AddForce(Direction * baseForceMagnitude * ForceMultiplier, ForceMode2D.Impulse);

    }
}