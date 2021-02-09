using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private PlayerController player;

    // unique unsigned identifier: básicamente es un identificador que hace único
    // cada start point, esto para saber en cual quiero aparecer
    public string uuid;

    // variable que controla a donde mirará el jugador al entrar en una nueva escena
    //public Vector2 facingDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        // Si el uuid siguiente no coincide con el uuid actual
        // NO es el lugar al que queríamos teletransportarnos
        if (!player.nextUuid.Equals(uuid))
        {
            return;
        }

        // asigna la posición del gameObject punto de inicio (StartPoint) al del player
        player.transform.position = this.transform.position;

        
    }
}
