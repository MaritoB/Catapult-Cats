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
    public int damage = 1;
    public Element element;
    public ParticleSystem ps;
    public bool onCatapult = true;

    private void Awake()
    {
    }

    private void Update()
    {
        if(ps != null)
        {
            ps.transform.position = transform.position;
        }
    }
    public void ResetLifeTime()
    {
        ps.gameObject.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
