using UnityEngine;

public class FireProjectile : Projectile
{
    public float explosionForce = 100f;
    public float explosionRadius = 5f;
    public ParticleSystem explosionPS;

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            MaterialType material = collider.GetComponent<MaterialType>();
            if (rb != null)
            {
                Vector2 explosionDirection = rb.transform.position - transform.position;
                float distance = explosionDirection.magnitude;
                float explosionPower = 1 - (distance / explosionRadius);
                rb.AddForce(explosionDirection.normalized * explosionForce * explosionPower, ForceMode2D.Impulse);
            }
            if(material != null)
            {
                material.ReactToCollision(element);
            }
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
