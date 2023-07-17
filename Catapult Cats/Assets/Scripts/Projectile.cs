using UnityEngine;
public enum Element
{
    Fire,
    Ice,
    Metal,
    Rock
}
public class Projectile : MonoBehaviour
{
    public GameObject body;
    public Rigidbody2D rb;
    private int ammo;
    private int _damage { get; set; }
    public  Element element { get; set; }
    protected Vector2 windForce;
    public void AddAmmo(int aValue)
    {
        ammo += aValue;
    }
    public void SetAmmo(int aValue)
    {
        ammo = aValue;
    }
    public int GetAmmo()
    {
        return ammo;
    }
    public void UseAmmo()
    {
        ammo--;
    }
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