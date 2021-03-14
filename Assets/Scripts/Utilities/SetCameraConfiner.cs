using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCameraConfiner : MonoBehaviour
{
    [SerializeField] TagID confinerTag;
    public void SetCameraBoundary()
    {
        var virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera");

        var confiner = virtualCamera.GetComponent<CinemachineConfiner>();

        confiner.m_BoundingShape2D = null;
        confiner.InvalidatePathCache();
        
        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag(confinerTag.ToString()).GetComponent<Collider2D>();
    }
}
