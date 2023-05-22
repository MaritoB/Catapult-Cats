using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeProjectile : Projectile
{
    [SerializeField]
    private float cuttingForce = 1f;

    private void Start()
    {
        rb = body.GetComponent<Rigidbody2D>();
        element = Element.Metal;
        body.SetActive(false);
    }

    public override void LaunchProyectile(Vector3 SpawnPosition, Vector3 aForce)
    {
        rb.gravityScale = 1;
        rb.angularVelocity = -500f;
        rb.velocity = Vector2.zero;
        body.transform.position = SpawnPosition;
        rb.AddForce(aForce, ForceMode2D.Impulse);
    }
    private void Update()
    {
    }

    public override void HandleCollision(Collision2D aCollision)
    {
 

        if (aCollision.relativeVelocity.magnitude < cuttingForce)
        {
            return;
        }
        MaterialType material = aCollision.collider.GetComponentInParent<MaterialType>();
        if (material != null)
        {
            material.ReactToCollision(element);
        }
    }

    public override void SetProjectileToShoot(Vector3 SpawnPosition)
    {
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        body.SetActive(true);
        //ps.gameObject.SetActive(true);
        body.transform.position = SpawnPosition;
    }
}
