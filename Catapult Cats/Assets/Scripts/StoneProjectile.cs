using UnityEngine;

public class StoneProjectile : Projectile
{

    public ParticleSystem ImpactPS;
    [SerializeField]
    private float life;
    [SerializeField]
    private float CurrentLife;

    private void Start()
    {
        rb = body.GetComponent<Rigidbody2D>();
        element = Element.Rock;
        body.SetActive(false);
        CurrentLife = life;
    }
    private void Update()
    {
    }
    public override void HandleCollision(Collision2D aCollision)
    {
        CurrentLife -= aCollision.relativeVelocity.magnitude;
        ImpactPS.transform.position = body.transform.position;
        ImpactPS.Emit(1);
        GameManager.Instance.CameraController.TurnToCastleCamera();
        if(CurrentLife < 0)
        {
            ImpactPS.Emit(15);
            body.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(windForce);
    }
    public override void SetProjectileToShoot(Vector3 SpawnPosition)
    {
        body.SetActive(true);
        body.transform.position = SpawnPosition;
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        CurrentLife = life;
    }
    public override void LaunchProyectile(Vector3 aSpawnPosition, Vector3 aForce, Vector2 aWind)
    {
        rb.gravityScale = 1;
        rb.angularVelocity = -500f;
        rb.velocity = Vector2.zero;
        body.transform.position = aSpawnPosition;
        windForce = aWind;
        rb.AddForce(aForce, ForceMode2D.Impulse);
    }



}
