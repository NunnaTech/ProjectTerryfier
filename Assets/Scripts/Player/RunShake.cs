using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class RunShake : MonoBehaviour
{
    [field: SerializeField] float powerAmount;
    [field: SerializeField] float currentAmount;
    [field: SerializeField] CinemachineFreeLook camera;
    
    public void CameraRun()
    {
        camera.m_Lens.FieldOfView = powerAmount;
    }
    public void CameraWalk()
    {
        camera.m_Lens.FieldOfView = currentAmount;
    }
    
}
