using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform Projectile; // Objeto que seguirá la cámara
    [SerializeField] private GameObject cannonTransform; // Objeto que representa el cañón
    [SerializeField] private float smoothSpeed = 2.5f; // Velocidad de la cámara
    [SerializeField] private float offset = 3f; // Distancia de la cámara respecto al objeto que sigue
    [SerializeField] private float minCameraX = -10f; // Límite mínimo de la cámara en el eje X
    [SerializeField] private float maxCameraX = 10f; // Límite máximo de la cámara en el eje X
    [SerializeField] private float minCameraY = -1f; // Altura mínima de la cámara
    [SerializeField] private float maxCameraY = 0.2f; // Altura máxima de la cámara
    [SerializeField] private float Speed = 10f; // Velocidad de movimiento rápido
    [SerializeField] private float minCameraZ = -100f; // Límite mínimo de la cámara en el eje Z
    [SerializeField] private float maxCameraZ = 100f; // Límite máximo de la cámara en el eje Z


    private void Start()
    {
        Projectile = cannonTransform.GetComponent<Catapult>().projectile.body.transform;
    }
    private void LateUpdate()
    {
        if (Projectile == null)
        {
            return;
           // Projectile = cannonTransform.GetComponent<Catapult>().projectile.transform;
        }
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(Projectile.position.x, minCameraX, maxCameraX),
            Mathf.Clamp(Projectile.position.y + offset, minCameraY, maxCameraY),
            Mathf.Clamp(transform.position.z, minCameraZ, maxCameraZ)
            );

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Speed * Time.deltaTime);
        transform.position = smoothedPosition;

    }
}