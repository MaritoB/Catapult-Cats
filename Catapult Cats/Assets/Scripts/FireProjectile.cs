using UnityEngine;

public class FireProjectile : Projectile
{
    public float explosionForce = 100f;
    public float explosionRadius = 5f;

    public ParticleSystem explosionPS;
    public ParticleSystem firePS;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        element = Element.Fire;
        rigidbody = body.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        firePS.transform.position = body.transform.position;
    }
    public override void HandleCollision(Collision2D aCollision)
    {
        explosionPS.transform.position = body.transform.position;
        explosionPS.Emit(40);
        firePS.Stop();
        Explode();
        body.SetActive(false);
    }

    public override void SetProjectileToShoot(Vector3 SpawnPosition)
    {
        body.SetActive(true);
        body.transform.position = SpawnPosition;
        rigidbody.angularVelocity = 0;
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0;
        firePS.gameObject.SetActive(true);
    }
    public override void LaunchProyectile(Vector3 SpawnPosition, Vector3 aForce)
    {
        rigidbody.gravityScale = 1;
        rigidbody.angularVelocity = -500f;
        rigidbody.velocity =Vector2.zero;
        body.transform.position = SpawnPosition;
        rigidbody.AddForce(aForce, ForceMode2D.Impulse);
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(body.transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 explosionDirection = rb.transform.position - body.transform.position;
                float distance = explosionDirection.magnitude;
                float explosionPower = 1 - (distance / explosionRadius);
                rb.AddForce(explosionDirection.normalized * explosionForce * explosionPower, ForceMode2D.Impulse);
            }
            MaterialType material = collider.GetComponentInParent<MaterialType>();
            if(material != null)
            {
                material.ReactToCollision(element);
            }

        }
    }

}
