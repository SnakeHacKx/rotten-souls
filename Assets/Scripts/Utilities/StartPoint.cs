using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private PlayerController player;

    // variable que controla a donde mirará el jugador al entrar en una nueva escena
    public Vector2 facingDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        // asigna la posición del gameObject punto de inicio (StartPoint) al del player
        player.transform.position = this.transform.position;

        player.lastMovement = facingDirection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
