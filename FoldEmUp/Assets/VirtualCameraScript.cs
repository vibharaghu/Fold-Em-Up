using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraScript : MonoBehaviour
{
    public GameObject player;
    public CinemachineVirtualCamera virtualCamera;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = player.transform;
    }

}
