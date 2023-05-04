using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    EnemyBase enemy;
    void Start()
    {
        enemy = GetComponentInParent<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemy.ReciveDamage(collision.relativeVelocity.magnitude);
    }
}
