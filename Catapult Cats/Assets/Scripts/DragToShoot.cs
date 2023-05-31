using UnityEngine;

public class DragToShoot : MonoBehaviour
{
    public float maxDragDistance = 5f;
    public float forceMagnitude;
    private Catapult catapult;
    private bool isDragging = false;
    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private Vector2 dragDirection;
    private float dragDistance;
    public Trajectory trajectory;    



    void Start()
    {
        catapult = GetComponent<Catapult>();
    }

    void Update()
    {
        if(catapult.CanShoot)
        {
            if (Input.GetMouseButtonDown(0)) // Si el jugador comienza a arrastrar
            {
                catapult.setupProjectile();
                trajectory.Show();
                isDragging = true;
                dragStartPosition = Input.mousePosition; // Guardar la posición de inicio del arrastre
            
            }

            if (isDragging) // Si el jugador sigue arrastrando
            {
                dragEndPosition = Input.mousePosition; // Guardar la posición final del arrastre
                dragDistance = Vector2.Distance(dragStartPosition, dragEndPosition); // Calcular la distancia del arrastre
                dragDistance = Mathf.Clamp(dragDistance, 0f, maxDragDistance); // Limitar la distancia máxima permitida para arrastrar
                dragDirection = (dragStartPosition - dragEndPosition);
                trajectory.UpdateDots(catapult.aim.transform.position, dragDirection * forceMagnitude);

            }

            if (Input.GetMouseButtonUp(0)) // Si el jugador suelta el arrastre
            {
                catapult.CastProjectile(dragDirection, dragDistance);
                isDragging = false;
                trajectory.Hide();
            }


        }
    }


}
