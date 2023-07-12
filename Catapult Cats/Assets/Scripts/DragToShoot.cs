using UnityEngine;

public class DragToShoot : MonoBehaviour
{
    [SerializeField]
    private Catapult catapult;
    private bool isDragging = false;
    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private Vector2 dragDirection;
    [SerializeField]
    private float maxDragPercentage = 40f;
    [SerializeField]
    private float TrajectoryMultiplier;
    public float dragDistance;
    public Vector2 mousePosition;
    public Trajectory trajectory;
    Vector2 Resolution;



    void Start()
    {
        Resolution.x = Screen.currentResolution.width;
        Resolution.y = Screen.currentResolution.height;
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        if(catapult.CanShoot())
        {
            if (Input.GetMouseButtonDown(0)) // Si el jugador comienza a arrastrar
            {
                    catapult.setupProjectile();
                    isDragging = true;
                    dragStartPosition = mousePosition;
                    trajectory.Show();
                    GameManager.Instance.CameraController.TurnToCatapultCamera();
                    catapult.CatAimAnimation();

            }

            

            if (isDragging) // Si el jugador sigue arrastrando
            {
 
                    dragEndPosition = mousePosition; // Guardar la posición final del arrastre
                    dragDistance =  Vector2.Distance((dragStartPosition*100)/Resolution, (dragEndPosition*100)/Resolution) ; // Calcular la distancia del arrastre
                    dragDistance = Mathf.Clamp(dragDistance, 0, maxDragPercentage);
                    dragDirection = (dragStartPosition - dragEndPosition).normalized;
                    trajectory.UpdateDots(catapult.aim.transform.position, dragDirection * dragDistance  * TrajectoryMultiplier);
            }

            if (Input.GetMouseButtonUp(0)) // Si el jugador suelta el arrastre
            {
                isDragging = false;
                trajectory.Hide();
                if(dragDirection.x > 0 && dragDistance >15f)
                {
                    catapult.CastProjectile(dragDirection, dragDistance );   
                    catapult.CatLaunchAnimation();

                }
                else
                {
                    catapult.CatIdleAnimation();
                }
                dragDirection = Vector2.zero;
                dragDistance = 0f;
            }
        }
    }

}
