using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera CatapultCamera, ProyectilCamera, CastleCamera;
    public void TurnToCatapultCamera()
    {
        //CatapultCamera.gameObject.SetActive(true);
        CatapultCamera.enabled = true;
        ProyectilCamera.enabled = false;
        CastleCamera.enabled = false;
    }
    public void TurnToProjectilCamera()
    {
        //CatapultCamera.gameObject.SetActive(true);
        CatapultCamera.enabled = false;
        ProyectilCamera.enabled = true;
        CastleCamera.enabled = false;
    }
    public void TurnToCastleCamera()
    {
        //CatapultCamera.gameObject.SetActive(true);
        CatapultCamera.enabled = false;
        ProyectilCamera.enabled = false;
        CastleCamera.enabled = true;
    }
}