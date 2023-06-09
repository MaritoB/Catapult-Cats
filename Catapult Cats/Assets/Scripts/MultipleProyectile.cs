using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleProyectile : Projectile
{
    public Projectile[] subProjectiles;
    public ParticleSystem smokePS;
    [SerializeField]
    private float ProjectileDispersion, ForceDispersionMultiplier, DispersionAngle;
    bool canLaunchSubprojectiles = false;
    Vector2 wind;

    private void Start()
    {
        element = Element.Metal;
        rb = body.GetComponent<Rigidbody2D>();
        body.SetActive(false);

        for (int i = 0; i < subProjectiles.Length; i++)
        {
            Projectile p = subProjectiles[i];
            p.body.gameObject.SetActive(false);
            //p.rb.angularVelocity = 0;
            //p.rb.velocity = Vector2.zero;
            //p.rb.gravityScale = 0;
        }

    }
    private void FixedUpdate()
    {
        rb.AddForce(windForce);
    }
    public override void LaunchProyectile(Vector3 aSpawnPosition, Vector3 aForce, Vector2 aWind)
    {
        rb.gravityScale = 1;
        rb.angularVelocity = -500f;
        rb.velocity = Vector2.zero;
        body.transform.position = aSpawnPosition;
        rb.AddForce(aForce, ForceMode2D.Impulse);
        windForce = aWind;
        canLaunchSubprojectiles=true;
    }
    
    public void ActivateSubProjectiles()
    {
        GameManager.Instance.CameraController.TurnToCastleCamera();
        canLaunchSubprojectiles =false;
        smokePS.gameObject.SetActive(true);
        smokePS.gameObject.transform.position = body.transform.position + (Vector3)rb.velocity.normalized;
        smokePS.Emit(30);
        for (int i = 0; i < subProjectiles.Length; i++)
        {
            Projectile p = subProjectiles[i];
            p.body.gameObject.SetActive(true);
            p.body.transform.position = rb.transform.position + Vector3.up * ProjectileDispersion * i;
            p.rb.velocity = rb.velocity;
            p.rb.gravityScale = 1;
            p.rb.angularVelocity = -500f;
            float angle = DispersionAngle * i;
            Vector3 dispersionVector = Quaternion.Euler(0f, 0f, angle) * p.rb.velocity.normalized;
            Vector3 newDirection = dispersionVector.normalized;
            Vector3 force = newDirection * ForceDispersionMultiplier * p.rb.mass;
            p.rb.AddForce(force, ForceMode2D.Impulse);
        }

        body.SetActive(false);
    }
    private void Update()
    {
        if (canLaunchSubprojectiles)
        {

            if (Input.GetMouseButtonDown(0)) 
            {
                ActivateSubProjectiles();
            }
        }
    }


    public override void HandleCollision(Collision2D aCollision)
    {
        ActivateSubProjectiles();
  
    }

    public override void SetProjectileToShoot(Vector3 SpawnPosition)
    {
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        body.SetActive(true);
        //ps.gameObject.SetActive(true);
        body.transform.position = SpawnPosition;
        canLaunchSubprojectiles = false;

        for (int i = 0; i < subProjectiles.Length; i++)
        {
            Projectile p = subProjectiles[i];
            p.rb.angularVelocity = 0;
            p.rb.velocity = Vector2.zero;
            p.rb.gravityScale = 0;
            p.body.gameObject.SetActive(false);
        }

    }
    public override void TurnOffProjectile()
    {
        body.SetActive(false);
        smokePS.gameObject.SetActive(false);
        for (int i = 0; i < subProjectiles.Length; i++)
        {
            Projectile p = subProjectiles[i];
            p.rb.angularVelocity = 0;
            p.rb.velocity = Vector2.zero;
            p.rb.gravityScale = 0;
            p.body.gameObject.SetActive(false);
        }
    }
}
