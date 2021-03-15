using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Controla lo referente al punto de aparición del jugador al cargar una nueva escena.
/// </summary>
public class StartPoint : MonoBehaviour
{
    private PlayerController player;
    //private AudioManager audioManager;

    // unique unsigned identifier: básicamente es un identificador que hace único
    // cada start point, esto para saber en cual quiero aparecer
    // public string uuid;

    [Tooltip("Punto de inicio de la escena")]
    [SerializeField] private StartAndTeleportPointsID uuid;

    private SetCameraConfiner _setCameraConfiner;

    // variable que controla a donde mirará el jugador al entrar en una nueva escena
    //public Vector2 facingDirection = Vector2.zero;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        _setCameraConfiner = GetComponent<SetCameraConfiner>();   

        // Si el uuid siguiente no coincide con el uuid actual
        // NO es el lugar al que queríamos teletransportarnos
        if (!player.nextUuid.Equals(uuid.ToString()))
        {
            return;
        }

        // asigna la posición del gameObject punto de inicio (StartPoint) al del player
        player.transform.position = this.transform.position;
        _setCameraConfiner.SetCameraBoundary();
        //audioManager.GetComponent<AudioManager>().PlayEnvironmentSound();
    }
}
