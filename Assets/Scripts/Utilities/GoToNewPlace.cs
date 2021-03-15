using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla el cambio de escenas y los puntos de inicio y teletransporte.
/// <list type="bullet">
/// <item>
/// <term>Teleport</term>
/// <description>Teletransporta al jugador al punto especificado.</description>
/// </item>
/// </list>
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class GoToNewPlace : MonoBehaviour
{
    [Tooltip("Escena a la que el jugador será teletransportado")]
    [SerializeField] private SceneID levelToLoad;

    [Tooltip("¿Necesita el jugador una tecla especificada para irse a una nueva escena?")]
    [SerializeField] private bool needsClick;

    //private SetCameraConfiner _setCameraConfiner;

    // identificador del start point al cual se quiere ir
    // public string uuid;

    [Tooltip("Punto de inicio o teletransporte")]
    [SerializeField] private StartAndTeleportPointsID uuid;

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

    /// <summary>
    /// Teletransporta al jugador a una nueva escena.
    /// </summary>
    /// <param name="objectTag">Contiene el tag de la colisión.</param>
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
                FindObjectOfType<PlayerController>().nextUuid = uuid.ToString();

                SceneManager.LoadScene(levelToLoad.ToString());
            }
        }     
    }
}
