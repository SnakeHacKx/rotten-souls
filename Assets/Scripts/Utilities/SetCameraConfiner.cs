using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Coloca los confinadores de la cámara al momento de cargar una nueva escena.
/// </summary>
public class SetCameraConfiner : MonoBehaviour
{
    [SerializeField] TagID confinerTag;

    /// <summary>
    /// Se encarga de colocar los confinadores de la cámara al momento de cargar una nueva escena.
    /// </summary>
    public void SetCameraBoundary()
    {
        var virtualCamera = GameObject.FindGameObjectWithTag(TagID.VirtualCamera.ToString());

        var confiner = virtualCamera.GetComponent<CinemachineConfiner>();

        confiner.m_BoundingShape2D = null;
        confiner.InvalidatePathCache();
        
        confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag(confinerTag.ToString()).GetComponent<Collider2D>();
    }
}
