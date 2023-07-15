using System.Collections;
using UnityEngine;
using FMODUnity;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject body;
    public float MaxLife;
    public float currentLife;
    public ParticleSystem ps;
    private GameManager gameManager;
    [SerializeField]
    private EventReference CrowSound;
    void Start()
    {
        currentLife = MaxLife;
        gameManager = GameManager.Instance;
        StartCoroutine(AddEnemyCount());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator AddEnemyCount()
    {
        yield return new WaitForSeconds(1);
        gameManager.AddEnemyCount();

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
        AudioManager.instance.PlayOneShot(CrowSound, transform.position);
        if (gameManager != null)
        {
            gameManager.EnemyKilled();
        }
    }
    private void PlayDeathSound()
    {
        /*
        if (deathSounds != null)
        {
            int index = Random.Range(0, deathSounds.Length);
            deathSounds[index].Play(); 

        }
         */
    }

}
