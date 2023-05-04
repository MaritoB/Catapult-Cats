using UnityEngine;

public class Target : MonoBehaviour
{
    public float minCollisionForce = 2f; // Fuerza mínima de la colisión para destruir el objetivo
    public GameObject explosionPrefab; // Prefab de la explosión
    public AudioClip explosionSound; // Sonido de la explosión

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= minCollisionForce) // Si la colisión tiene una fuerza suficiente para destruir el objetivo
        {
            gameObject.SetActive(false);
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Generar la explosión
        AudioSource.PlayClipAtPoint(explosionSound, transform.position); // Reproducir el sonido de la explosión
        Destroy(explosion, 1f); // Destruir la explosión después de 1 segundo
    }
}
