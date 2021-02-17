using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // si el player no ha sido creado, no lo destruyas al cambiar de escena
        if (!PlayerController.playerCreated)
        {
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            // si ya existe una copia del player, el último es destruido
            Destroy(gameObject);
        }
    }
}
