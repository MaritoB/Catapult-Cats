using UnityEngine;

public class FireProjectile : Projectile
{
    public float explosionForce = 100f;
    public float explosionRadius = 5f;
    public ParticleSystem explosionPS;

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        Debug.Log("Explode & hit : " + colliders.Length);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 explosionDirection = rb.transform.position - transform.position;
                float distance = explosionDirection.magnitude;
                float explosionPower = 1 - (distance / explosionRadius);
                rb.AddForce(explosionDirection.normalized * explosionForce * explosionPower, ForceMode2D.Impulse);
            }
            /*
            if (collider.CompareTag("Wood"))
            {
                Wood wood = collider.GetComponent<Wood>();
                if (wood != null)
                {
                    wood.Ignite();
                }
            }
             */
        }
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        explosionPS.transform.position = transform.position;
        explosionPS.Emit(40);
        ps.Stop();
        Explode();
    }
}
