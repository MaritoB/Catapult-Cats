using UnityEngine;
public enum Element
{
    Fire,
    Ice,
    Metal,
    Rock
}
public class Projectile: MonoBehaviour
{
    public GameObject body;
    public  Rigidbody2D rb;
    public int damage { get; set; }
    public  Element element { get; set; }
    protected Vector2 windForce;
    public virtual void HandleCollision(Collision2D aCollision)
    {
        Debug.Log("Projectil Base HandleCollision");
    }
    public virtual void SetProjectileToShoot(Vector3 SpawnPosition)
    {

        Debug.Log("Projectil Base SetProjectileToShoot");
    }
    public virtual void TurnOffProjectile()
    {

        Debug.Log("Projectil Base SetProjectileToShoot");
    }
    public virtual void LaunchProyectile(Vector3 SpawnPosition, Vector3 aForce, Vector2 wind)
    {

        Debug.Log("Projectil Base Launch");
    }

}