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
    public Trajectory trajectory;
    Vector2 Resolution;
    private bool isMobile;

    void Update()
    {
        if (isMobile)
        {
            UseTouchInput();
        }
        else
        {
            UseMouseInput();
        }
    }

    void Start()
    {
        Resolution.x = Screen.currentResolution.width;
        Resolution.y = Screen.currentResolution.height;
        isMobile = Application.isMobilePlatform;
    }
    void UseTouchInput()
    {
        if (catapult.CanShoot())
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began) 
                {
                    catapult.setupProjectile();
                    isDragging = true;
                    dragStartPosition = touch.position;
                    trajectory.Show();
                    GameManager.Instance.CameraController.TurnToCatapultCamera();
                    catapult.CatAimAnimation();
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    dragEndPosition = touch.position;
                    dragDistance = Vector2.Distance((dragStartPosition * 100) / Resolution, (dragEndPosition * 100) / Resolution); 
                    dragDistance = Mathf.Clamp(dragDistance, 0, maxDragPercentage);
                    dragDirection = (dragStartPosition - dragEndPosition).normalized;
                    trajectory.UpdateDots(catapult.aim.transform.position, dragDirection * dragDistance * TrajectoryMultiplier);
                }

                if (touch.phase == TouchPhase.Ended) 
                {
                    isDragging = false;
                    trajectory.Hide();
                    if (dragDirection.x > 0 && dragDistance > 15f)
                    {
                        catapult.CastProjectile(dragDirection, dragDistance);
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
    void UseMouseInput()
    {
        if (catapult.CanShoot())
        {
            if (Input.GetMouseButtonDown(0))
            {
                catapult.setupProjectile();
                isDragging = true;
                dragStartPosition = Input.mousePosition;
                trajectory.Show();
                GameManager.Instance.CameraController.TurnToCatapultCamera();
                catapult.CatAimAnimation();
            }
            if (isDragging) 
            {
                dragEndPosition = Input.mousePosition; ; 
                dragDistance = Vector2.Distance((dragStartPosition * 100) / Resolution, (dragEndPosition * 100) / Resolution);
                dragDistance = Mathf.Clamp(dragDistance, 0, maxDragPercentage);
                dragDirection = (dragStartPosition - dragEndPosition).normalized;
                trajectory.UpdateDots(catapult.aim.transform.position, dragDirection * dragDistance * TrajectoryMultiplier);
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                trajectory.Hide();
                if (dragDirection.x > 0 && dragDistance > 15f)
                {
                    catapult.CastProjectile(dragDirection, dragDistance);
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