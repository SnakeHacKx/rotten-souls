using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permite destruir un objeto después de pasado cierto tiempo
/// </summary>
public class DestroyAfterTime : MonoBehaviour
{
    [Tooltip("Al pasar este tiempo, el objeto será destruido")]
    [SerializeField] private float timeToDestroy;

    /// <summary>
    /// Destruye al objeto.
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    // OTRA FORMA DE DESTRUIR DESPUÉS DE UN TIEMPO

    //private void Update()
    //{
    //    timeToDestroy -= Time.deltaTime;

    //    if (timeToDestroy < 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
