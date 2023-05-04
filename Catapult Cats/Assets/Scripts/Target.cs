using UnityEngine;

public class Target : MonoBehaviour
{
    public float minCollisionForce = 2f; // Fuerza m�nima de la colisi�n para destruir el objetivo
    public GameObject explosionPrefab; // Prefab de la explosi�n
    public AudioClip explosionSound; // Sonido de la explosi�n

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= minCollisionForce) // Si la colisi�n tiene una fuerza suficiente para destruir el objetivo
        {
            gameObject.SetActive(false);
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Generar la explosi�n
        AudioSource.PlayClipAtPoint(explosionSound, transform.position); // Reproducir el sonido de la explosi�n
        Destroy(explosion, 1f); // Destruir la explosi�n despu�s de 1 segundo
    }
}
