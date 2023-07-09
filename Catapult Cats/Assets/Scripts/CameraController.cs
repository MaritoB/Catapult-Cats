using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera CatapultCamera, ProjectilCamera, CastleCamera;
    private void Start()
    {
        TurnToCatapultCamera();
    }
    public void TurnToCatapultCamera()
    {
        //CatapultCamera.gameObject.SetActive(true);
        CatapultCamera.enabled = true;
        ProjectilCamera.enabled = false;
        CastleCamera.enabled = false;
    }
    public void SetFollowProjectile(GameObject aTransformProjectile)
    {
        ProjectilCamera.Follow = aTransformProjectile.transform;
    }
    public void TurnToProjectilCamera()
    {
        //CatapultCamera.gameObject.SetActive(true);
        CatapultCamera.enabled = false;
        ProjectilCamera.enabled = true;
        CastleCamera.enabled = false;
    }
    public void TurnToCastleCamera()
    {
        //CatapultCamera.gameObject.SetActive(true);
        CatapultCamera.enabled = false;
        ProjectilCamera.enabled = false;
        CastleCamera.enabled = true;
    }
}