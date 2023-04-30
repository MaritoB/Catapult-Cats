using UnityEngine;

public class AutoShoot : MonoBehaviour
{

    private Catapult catapult;
    public float shootInterval = 1.0f;
    public float launchAngle = 45.0f;
    public float launchForce = 10.0f;
    private float timer = 0.0f;

    private void Start()
    {
        catapult = GetComponent<Catapult>();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Vector2 launchDirection = Quaternion.Euler(0, 0, launchAngle) * Vector2.right;
            catapult.CastProjectile(launchDirection, launchForce);
            timer = 0.0f;
        }
    }
}