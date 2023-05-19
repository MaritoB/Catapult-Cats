using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeProjectile : Projectile
{
    private Rigidbody2D rigidbody;
    [SerializeField]
    private float cuttingForce = 1f;

    private void Start()
    {
        rb = body.GetComponent<Rigidbody2D>();
        element = Element.Metal;
    }

    public override void LaunchProyectile(Vector3 SpawnPosition, Vector3 aForce)
    {
        rigidbody.gravityScale = 1;
        rigidbody.angularVelocity = -500f;
        rigidbody.velocity = Vector2.zero;
        body.transform.position = SpawnPosition;
        rigidbody.AddForce(aForce, ForceMode2D.Impulse);
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
        rigidbody.angularVelocity = 0;
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0;
        body.SetActive(true);
        //ps.gameObject.SetActive(true);
        body.transform.position = SpawnPosition;
    }
}
