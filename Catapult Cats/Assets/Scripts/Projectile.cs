using UnityEngine;
public enum Element
{
    Fire,
    Wood,
    Ice,
    Metal,
    Rock
}
public class Projectile : MonoBehaviour
{
    public float lifetime = 10f;
    private float currentLifeTime = 0;
    public int damage = 1;
    public Element element;
    public ParticleSystem ps;
    public bool onCatapult = true;

    private void Awake()
    {
        currentLifeTime = lifetime;
    }

    private void Update()
    {
        if(ps != null)
        {
            ps.transform.position = transform.position;
        }
        if (onCatapult) return;
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime < 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void ResetLifeTime()
    {
        ps.gameObject.SetActive(true);
        currentLifeTime = lifetime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
