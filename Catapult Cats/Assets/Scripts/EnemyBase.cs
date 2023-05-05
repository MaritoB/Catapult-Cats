using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject body;
    public float MaxLife;
    public float currentLife;
    public ParticleSystem ps;
    [SerializeField]
    private AudioSource[] deathSounds;
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
        PlayDeathSound();
    }
    private void PlayDeathSound()
    {
        if (deathSounds != null)
        {
            int index = Random.Range(0, deathSounds.Length);
            deathSounds[index].Play(); 

        }
    }
}
