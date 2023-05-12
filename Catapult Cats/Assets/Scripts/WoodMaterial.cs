using System.Collections;
using UnityEngine;

public class WoodMaterial : MonoBehaviour, MaterialType
{
    public GameObject body;
    private SpriteRenderer sprite;

    public ParticleSystem fireParticles;
    public ParticleSystem iceParticles;
    public ParticleSystem damageParticles;
    public ParticleSystem destroyParticles;
    public ParticleSystem SmokeParticles;

    public float IgniteTime;
    public float IgniteOthersTime = 1;
    public float IgniteRadius;
    private bool isBurning = false;
    private float colorLerpT;
    public float burningDamage;

    private void Start()
    {
        sprite = body.GetComponent<SpriteRenderer>();

    }
    void adjustPositions()
    {
        Vector3 position = body.transform.position;
        fireParticles.transform.position = position;
        //iceParticles.transform.position = position;
        //damageParticles.transform.position = position;
        SmokeParticles.transform.position = position;
    }
    private void Update()
    {
        adjustPositions();
        if (isBurning)
        {
            sprite.color = Color.Lerp(Color.white, Color.black, colorLerpT);
            if (colorLerpT < 1)
            {
                colorLerpT += Time.deltaTime / IgniteTime;
            }
            if(IgniteOthersTime > 0)
            {
                IgniteOthersTime -= Time.deltaTime;

            }
            else
            {
                IgniteNearWood();
                IgniteOthersTime = 0.3f;
            }

        }
    }
    public void ReactToCollision(Element otherElement)
    {
        switch (otherElement)
        {
            case Element.Fire:
                // Wood ignites when it collides with fire
                Ignite();
                break;
            case Element.Ice:
                // Wood freezes when it collides with ice
                Freeze();
                break;
            case Element.Metal:
                // Wood gets damaged when it collides with metal
                Damage();
                break;
            case Element.Rock:
                // Wood gets destroyed when it collides with rock
                Destroy();
                break;
        }
    }

    public void Ignite()
    {
        if (isBurning)
        {
            return;
        }
        isBurning = true;
        fireParticles.gameObject.SetActive(true);
        fireParticles.Play();
        SmokeParticles.gameObject.SetActive(true);
        SmokeParticles.Emit(30);
        StartCoroutine(DisableOnSeconds());
    }
    private void IgniteNearWood()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(body.transform.position, IgniteRadius);
        foreach (Collider2D collider in colliders)
        {
            WoodMaterial material = collider.GetComponentInParent<WoodMaterial>();
            if(material != null && material != this)
            {
                if (material.isBurning)
                {
                    continue;
                }
                Debug.Log(gameObject.name + " Ignite " + material.gameObject.name);
                material.Ignite();
            }
            EnemyBase enemy = collider.GetComponentInParent<EnemyBase>();
            if(enemy != null)
            {
                enemy.ReciveDamage(burningDamage);
            }

        }
    }
    private IEnumerator DisableOnSeconds()
    {
        yield return new WaitForSeconds(IgniteTime);
        isBurning = false;
        body.SetActive(false);
        sprite.color = Color.white;
        SmokeParticles.gameObject.SetActive(true);
        SmokeParticles.Emit(30);
        fireParticles.Stop();
        //StartCoroutine(turnOff());


    }
    private IEnumerator turnOff()
    {
        yield return new WaitForSeconds(3);
        body.SetActive(false);
        sprite.color = Color.white;

    }

    private void Freeze()
    {
        iceParticles.Play();
    }

    private void Damage()
    {
        body.SetActive(false);
    }

    private void Destroy()
    {
        destroyParticles.Play();
        gameObject.SetActive(false);
    }
}
