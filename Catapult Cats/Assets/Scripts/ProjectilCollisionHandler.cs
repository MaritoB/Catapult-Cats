using UnityEngine;

public class ProjectilCollisionHandler : MonoBehaviour
{
    Projectile projectile;
    private void Start()
    {
        projectile = GetComponentInParent<Projectile>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject == this.gameObject)
        {
            Debug.Log("Collision propia");
            return;
        }
        projectile.HandleCollision(collision);
    }
}

    
