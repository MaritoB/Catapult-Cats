using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject body;
    public float MaxLife;
    public float currentLife;
    public ParticleSystem ps;
    void Start()
    {
        currentLife = MaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReciveDamage(float aDamage)
    {
        currentLife -= aDamage;
        if (currentLife < 0)
        {
            Die();
        }
    }
    private void Die()
    {
        ps.transform.position = body.transform.position;
        body.SetActive(false);
        ps.Emit(30);
    }
}
