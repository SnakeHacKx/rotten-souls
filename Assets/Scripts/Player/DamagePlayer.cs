using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el daño hacia el jugador.
/// <list type="bullet">
/// <item>
/// <term>OnCollisionEnter2D</term>
/// <description>Detecta si hay alguna colisión contra el jugador.</description>
/// </item>
/// </list>
/// </summary>
public class DamagePlayer : MonoBehaviour
{
    //public float timeToRevivePlayer;
    //private float timeRevivalCounter;
    //private bool playerReviving;

    [Tooltip("Daño que hace el enemigo")]
    [SerializeField] private int damage;

    //private GameObject thePlayer;

    /// <summary>
    /// Detecta si hay alguna colisión contra el jugador y le manda el daño que recibe,
    /// por parte del enemigo, al mánager de la vida.
    /// </summary>
    /// <param name="collision">Guarda los datos de la colisión</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerReviving)
        //{
        //    timeRevivalCounter -= Time.deltaTime;

        //    if(timeRevivalCounter < 0)
        //    {
        //        playerReviving = false;
        //        thePlayer.SetActive(true);
        //    }
        //}
    }
}
