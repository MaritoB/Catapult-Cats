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
    public int damage { get; set; }
    public  Element element { get; set; }
    public void Start()
    {
        body = GetComponentInChildren<Rigidbody2D>().gameObject;
    }
    public virtual void HandleCollision(Collision2D aCollision)
    {
        Debug.Log("Projectil Base HandleCollision");
    }
    public virtual void SetProjectileToShoot(Vector3 SpawnPosition)
    {

        Debug.Log("Projectil Base SetProjectileToShoot");
    }
    public virtual void LaunchProyectile(Vector3 SpawnPosition, Vector3 aForce)
    {

        Debug.Log("Projectil Base Launch");
    }

}