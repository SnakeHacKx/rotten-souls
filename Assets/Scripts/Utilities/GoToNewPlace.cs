using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class GoToNewPlace : MonoBehaviour
{
    [SerializeField]
    private string newPlaceName = "New Scene Name Here!!!";

    [SerializeField]
    [Tooltip("¿Necesita el jugador pulsar la tecla E para irse a un nuevo lugar?")]
    private bool needsClick;

    //private SetCameraConfiner _setCameraConfiner;

    // identificador del start point al cual se quiere ir
    public string uuid;

    //private void Start()
    //{
    //    _setCameraConfiner = GetComponent<SetCameraConfiner>();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        Teleport(collision.gameObject.tag);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Teleport(collision.gameObject.tag);
    }

    private void Teleport(string objectTag)
    {
        // Si el tag del gameObject que entra en collision con el collider que tenga
        // este script es "Player", entonces cargo una nueva escena
        if (objectTag == "Player")
        {
            if (!needsClick || (needsClick && Input.GetKeyDown(KeyCode.E)))
            {
                // indica que el siguiente punto de teletransporte es el que se le ha
                // puesto al GoToNewPlace
                FindObjectOfType<PlayerController>().nextUuid = uuid;

                SceneManager.LoadScene(newPlaceName);
            }
        }     
    }
}
