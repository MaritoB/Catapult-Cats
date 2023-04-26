using System.Collections;
using UnityEngine;

public class WoodMaterial : MonoBehaviour, MaterialType
{
    public ParticleSystem fireParticles;
    public ParticleSystem iceParticles;
    public ParticleSystem damageParticles;
    public ParticleSystem destroyParticles;
    public ParticleSystem SmokeParticles;
    public float IgniteTime;
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

    private void Ignite()
    {
        fireParticles.Play();
        StartCoroutine(DisableOnSeconds());
    }
    private IEnumerator DisableOnSeconds()
    {
        yield return new WaitForSeconds(IgniteTime);
        gameObject.SetActive(false);
        SmokeParticles.transform.position = transform.position;
        SmokeParticles.Emit(30);
        fireParticles.Stop();
    }
    private void Freeze()
    {
        iceParticles.Play();
    }

    private void Damage()
    {
        damageParticles.Play();
    }

    private void Destroy()
    {
        destroyParticles.Play();
        gameObject.SetActive(false);
    }
}
