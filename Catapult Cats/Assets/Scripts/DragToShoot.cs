using UnityEngine;

public class DragToShoot : MonoBehaviour
{
    public float maxDragDistance = 5f;
    public float forceMagnitude;
    [SerializeField]
    private Catapult catapult;
    private bool isDragging = false;
    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private Vector2 dragDirection;
    private float dragDistance;
    public Trajectory trajectory;    



    void Start()
    {

    }

    void Update()
    {
        if(catapult.CanShoot)
        {
            if (Input.GetMouseButtonDown(0)) // Si el jugador comienza a arrastrar
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("DragToShoot"))
                {
                    catapult.setupProjectile();
                    trajectory.Show();
                    isDragging = true;
                    dragStartPosition = Input.mousePosition; // Guardar la posición de inicio del arrastre
                
                }

            }

            if (isDragging) // Si el jugador sigue arrastrando
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("DragToShoot"))
                {
                    dragEndPosition = Input.mousePosition; // Guardar la posición final del arrastre
                    dragDistance = Vector2.Distance(dragStartPosition, dragEndPosition); // Calcular la distancia del arrastre
                    dragDirection = (dragStartPosition - dragEndPosition).normalized;
                    trajectory.UpdateDots(catapult.aim.transform.position, dragDirection * dragDistance *  forceMagnitude);
                }

            }

            if (Input.GetMouseButtonUp(0)) // Si el jugador suelta el arrastre
            {
                isDragging = false;
                trajectory.Hide();
                if(dragDirection.x > 0)
                {
                    catapult.CastProjectile(dragDirection, dragDistance * forceMagnitude);   

                }
                dragDirection = Vector2.zero;
                dragDistance = 0f;
            }


        }
    }


}
