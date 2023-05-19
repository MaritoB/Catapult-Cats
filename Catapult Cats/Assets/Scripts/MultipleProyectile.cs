using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleProyectile : Projectile
{
    public Projectile[] subProjectiles;
    [SerializeField]
    private float ProjectileDispersion;
    bool canLaunchSubprojectiles = false;

    // Start is called before the first frame update

    private void Start()
    {
        element = Element.Metal;
        rb = body.GetComponent<Rigidbody2D>();
    }

    public override void LaunchProyectile(Vector3 SpawnPosition, Vector3 aForce)
    {
        rb.gravityScale = 1;
        rb.angularVelocity = -500f;
        rb.velocity = Vector2.zero;
        body.transform.position = SpawnPosition;
        rb.AddForce(aForce, ForceMode2D.Impulse);
        canLaunchSubprojectiles=true;

    }
    
    public void ActivateSubProjectiles()
    {
        canLaunchSubprojectiles=false;
        for (int i = 0; i < subProjectiles.Length; i++)
        {
            Projectile p = subProjectiles[i];
            p.body.gameObject.SetActive(true);
            p.body.transform.position = rb.transform.position +  Vector3.right * ProjectileDispersion * i ;
            p.rb.velocity = rb.velocity;
            p.rb.AddForce(Vector2.one * ProjectileDispersion * i, ForceMode2D.Impulse);
            p.rb.gravityScale = 1;
            p.rb.angularVelocity = -500f;
        }
        body.SetActive(false);
    }
    private void Update()
    {
        if (canLaunchSubprojectiles)
        {

            if (Input.GetMouseButtonDown(0)) // Si el jugador comienza a arrastrar
            {
                ActivateSubProjectiles();
            }
        }
    }


    public override void HandleCollision(Collision2D aCollision)
    {

    }

    public override void SetProjectileToShoot(Vector3 SpawnPosition)
    {
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        body.SetActive(true);
        //ps.gameObject.SetActive(true);
        body.transform.position = SpawnPosition;

        for(int i = 0; i < subProjectiles.Length; i++)
        {
            Projectile p = subProjectiles[i];
            p.body.gameObject.SetActive(false);
            p.rb.angularVelocity = 0;
            p.rb.velocity = Vector2.zero;
            p.rb.gravityScale = 0;
        }

    }
}
