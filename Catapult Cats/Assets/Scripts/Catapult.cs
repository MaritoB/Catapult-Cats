using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject projectile; // Objeto del proyectil de gato
    public Transform spawnPoint; // Punto de origen del proyectil

    private bool isDragging = false;
    private Vector3 dragStartPosition;
    private Vector3 dragEndPosition;
    private Vector3 dragDirection;
    private float dragDistance;
    public float maxDragDistance = 5f; // Distancia m�xima permitida para arrastrar
    public float forceMagnitude ;
    private Rigidbody2D rb;

    void Start()
    {
        rb = projectile.GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del proyectil
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Si el jugador comienza a arrastrar
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition; // Guardar la posici�n de inicio del arrastre
            projectile.transform.position = spawnPoint.position;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }

        if (isDragging) // Si el jugador sigue arrastrando
        {
            rb.velocity = Vector2.zero;
            dragEndPosition = Input.mousePosition; // Guardar la posici�n final del arrastre
            dragDistance = Vector3.Distance(dragStartPosition, dragEndPosition); // Calcular la distancia del arrastre
            dragDistance = Mathf.Clamp(dragDistance, 0f, maxDragDistance); // Limitar la distancia m�xima permitida para arrastrar
            dragDirection = (dragStartPosition - dragEndPosition);
            projectile.transform.position = spawnPoint.position - dragDirection.normalized ;
        }

        if (Input.GetMouseButtonUp(0)) // Si el jugador suelta el arrastre
        {
            isDragging = false;
            rb.gravityScale = 1;            
            rb.AddForce(dragDirection * forceMagnitude * dragDistance, ForceMode2D.Impulse); // Aplicar la fuerza al proyectil
            Debug.Log("lanzamiento " + dragDistance);
        }
    }
}
