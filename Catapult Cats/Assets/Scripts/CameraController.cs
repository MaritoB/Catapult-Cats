using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera CatapultCamera, ProjectilCamera, CastleCamera;
    public Cinemachine.CinemachineTargetGroup ProjectileGroup;
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
    public void SetFollowProjectile(Transform aNewProjectile)
    {
        ProjectilCamera.Follow = aNewProjectile;
        /*
        if(aOldProjectile == aNewProjectile)
        {
            return;
        }
        ProjectileGroup.RemoveMember(aOldProjectile);
        ProjectileGroup.AddMember(aNewProjectile, 1f, 10f);
         */
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