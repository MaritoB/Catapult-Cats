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
    public float ShootCooldown;
    private float ShootEstimatedTime = 3f;
    private float CurrentShootEstimatedTime = 0f;
    public Trajectory trajectory;    

    public float CurrentShootCooldown = 0f;


    void Start()
    {
        catapult = GetComponent<Catapult>();
    }

    void Update()
    {
        if (CurrentShootCooldown > 0)
        {
            CurrentShootCooldown -= Time.deltaTime;
            return;
        }
        else
        {
            catapult.setProjectileToShoot();
            if (Input.GetMouseButtonDown(0)) // Si el jugador comienza a arrastrar
            {
                trajectory.Show();
                isDragging = true;
                dragStartPosition = Input.mousePosition; // Guardar la posición de inicio del arrastre
            
                /*
                // create trajectory line
                if (lr != null)
                {
                    lr.enabled = true;
                    lr.positionCount = 2;
                    lr.SetPosition(0, spawnPoint.position);
                    lr.SetPosition(1, spawnPoint.position);
                }
                 */
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
                CurrentShootCooldown = ShootCooldown;
                CurrentShootEstimatedTime = ShootEstimatedTime;
                trajectory.Hide();
                // hide trajectory line
                /*
                if (lr != null)
                {
                    lr.enabled = false;
                }
                 */
            }

            // update trajectory line
            //lr.SetPosition(0, spawnPoint.position);
            //DrawTrajectory(spawnPoint.position, dragDirection.normalized * forceMagnitude, rb.gravityScale, dragDistance);


        }
    }


}
