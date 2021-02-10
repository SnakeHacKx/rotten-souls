using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy;

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
