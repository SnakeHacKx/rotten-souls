using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float timeToRevivePlayer;
    private float timeRevivalCounter;
    private bool playerReviving;

    [SerializeField]
    [Tooltip("Daño que hace el enemigo")]
    private int damage;

    private GameObject thePlayer;

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
        if (playerReviving)
        {
            timeRevivalCounter -= Time.deltaTime;

            if(timeRevivalCounter < 0)
            {
                playerReviving = false;
                thePlayer.SetActive(true);
            }
        }
    }
}
