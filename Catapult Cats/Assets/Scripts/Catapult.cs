using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject projectile;
    public GameObject aim;
    public Transform spawnPoint;

    public float maxDragDistance = 5f;
    public float forceMagnitude;

    private Animator animator;
    private bool isDragging = false;
    private Vector3 dragStartPosition;
    private Vector3 dragEndPosition;
    private Vector3 dragDirection;
    private float dragDistance;
    private bool canShoot = true;

    private Rigidbody2D rb;
    private LineRenderer lr;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = projectile.GetComponent<Rigidbody2D>();
        lr = aim.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (!canShoot)
        {
            projectile.transform.position = spawnPoint.position;
            return;
        }
        if (Input.GetMouseButtonDown(0)) // Si el jugador comienza a arrastrar
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition; // Guardar la posición de inicio del arrastre
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;

            // create trajectory line
            if (lr != null)
            {
                lr.enabled = true;
                lr.positionCount = 2;
                lr.SetPosition(0, spawnPoint.position);
                lr.SetPosition(1, spawnPoint.position);
            }
        }

        if (isDragging) // Si el jugador sigue arrastrando
        {
            rb.velocity = Vector2.zero;
            dragEndPosition = Input.mousePosition; // Guardar la posición final del arrastre
            dragDistance = Vector3.Distance(dragStartPosition, dragEndPosition); // Calcular la distancia del arrastre
            dragDistance = Mathf.Clamp(dragDistance, 0f, maxDragDistance); // Limitar la distancia máxima permitida para arrastrar
            dragDirection = (dragStartPosition - dragEndPosition);
            // update trajectory line
            if (lr != null)
            {
                lr.SetPosition(0, spawnPoint.position);
                DrawTrajectory(spawnPoint.position, dragDirection.normalized * forceMagnitude, rb.gravityScale, forceMagnitude);
            }
        }

        if (Input.GetMouseButtonUp(0)) // Si el jugador suelta el arrastre
        {
            canShoot = false;
            animator.SetTrigger("Launch");
            // hide trajectory line
            if (lr != null)
            {
                lr.enabled = false;
            }
        }
        
        // update trajectory line
        lr.SetPosition(0, spawnPoint.position);
        DrawTrajectory(spawnPoint.position, dragDirection.normalized * forceMagnitude, rb.gravityScale, dragDistance);

    }
    public void LaunchProyectil()
    {
        canShoot = true;
        isDragging = false;
        rb.gravityScale = 1;
        projectile.transform.position = spawnPoint.position;
        rb.AddForce(dragDirection * forceMagnitude * dragDistance, ForceMode2D.Impulse); // Aplicar la fuerza al proyectil
        Debug.Log("Lanzamiento " + dragDistance);

    }

    void DrawTrajectory(Vector3 startingPosition, Vector3 initialVelocity, float gravityScale, float arrowSize)
    {
        int maxSteps = 100;
        float timeStep = 0.02f;
        Vector3[] positions = new Vector3[maxSteps];
        Vector3 gravity = Physics2D.gravity * gravityScale;

        positions[0] = startingPosition;

        Vector3 currentPosition = startingPosition;
        Vector3 currentVelocity = initialVelocity;

        for (int i = 1; i < maxSteps; i++)
        {
            float currentTime = timeStep * i;
            currentPosition += currentVelocity * timeStep;
            currentVelocity += gravity * timeStep;
            positions[i] = currentPosition;
            if (currentPosition.y < -10f) break;
        }

        lr.positionCount = maxSteps;
        lr.SetPositions(positions);

        // Change the size of the arrow based on the force magnitude
        float size = arrowSize / 50f;
        lr.startWidth = size;
        lr.endWidth = size;
    }


}