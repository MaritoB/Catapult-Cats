using System.Collections;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Transform[] waypoints;
    public float[] timers;
    public float speed = 5.0f;

    private int _currentWaypointIndex = 0;
    private bool _isMoving = true;

    private void Start()
    {
        if (waypoints.Length != timers.Length)
        {
            Debug.LogError("Error: The number of waypoints must match the number of timers.");
            return;
        }

        StartCoroutine(MoveToWaypoints());
    }

    private IEnumerator MoveToWaypoints()
    {
        while (true)
        {
            if (_isMoving)
            {
                Transform currentWaypoint = waypoints[_currentWaypointIndex];
                float timer = timers[_currentWaypointIndex];
                float distance = Vector3.Distance(transform.position, currentWaypoint.position);
                float duration = distance / speed;

                while (timer > 0.0f)
                {
                    float t = Time.deltaTime / timer;
                    transform.position = Vector3.Lerp(transform.position, currentWaypoint.position, t);
                    timer -= Time.deltaTime;
                    yield return null;
                }

                transform.position = currentWaypoint.position;

                if (_currentWaypointIndex == waypoints.Length - 1)
                {
                    _currentWaypointIndex = 0;
                }
                else
                {
                    _currentWaypointIndex++;
                }

                yield return new WaitForSeconds(duration);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void StartMoving()
    {
        _isMoving = true;
    }

    public void StopMoving()
    {
        _isMoving = false;
    }
}
