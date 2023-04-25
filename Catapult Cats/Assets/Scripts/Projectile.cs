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
    public float lifetime = 10f;
    private float currentLifeTime = 0;
    public int damage = 1;
    public Element element;

    private void Awake()
    {
        currentLifeTime = lifetime;
    }

    private void Update()
    {
        currentLifeTime -= Time.deltaTime;
        if (currentLifeTime < 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void ResetLifeTime()
    {

        currentLifeTime = lifetime;
    }
    private void OnTriggerEnter(Collider other)
    {
       
    }
}
