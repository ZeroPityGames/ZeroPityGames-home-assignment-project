using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DebugOptions : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] PlayerMovment playerMovment;
    [SerializeField] GameManager gameManager;
    [SerializeField] Slider speedSlider;

    private void Start()
    {
        speedSlider.value = playerMovment.movmentSpeed;
    }
    public void CameraDistance(float distance)
    {
        CinemachineComponentBase componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = distance; // your value
        }
    }

    public void AddCash()
    {
        gameManager.IncressMoney(10000);
    }

    public void IncressSpeed()
    {
        playerMovment.movmentSpeed = speedSlider.value;
    }
}
