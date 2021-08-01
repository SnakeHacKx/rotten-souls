using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamageTeleport : MonoBehaviour
{
    [SerializeField] private int damagePoints = 1;
    [SerializeField] TagID targetTag;
    /*private ITargetCombat component;

    private void Start()
    {
        component = GetComponent<ITargetCombat>();
    }*/

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag.ToString()))
        {
            if (component != null)
            {
                component.TakeDamage(damagePoints);

                if (HeroController.SharedInstance.Health > 0)
                {
                    HeroController.SharedInstance.UpdatePosition(HeroController.SharedInstance.LastPositionOnGround);
                }
            }
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag.ToString()))
        { 
            if (Player.Instance != null)
            {
                PlayerTakesDamage.Instance.TakeSpikeDamage(damagePoints);
            }
        }
    }

    public void SendPlayerToRespawnPosition()
    {
        Invoke(nameof(SendPlayerToLastGroundPosition), 0.5f);
    }

    private void SendPlayerToLastGroundPosition()
    {
        if (HealthManager.Instance.Health > 0)
        {

            Player.Instance.ChangePosition(Player.Instance.LastPositionOnGround);
            Debug.Log("Debio respawnear en la posicion: " + Player.Instance.LastPositionOnGround);
        }
    }

    /*public void ApplySpikeDamage()
    {
        StartCoroutine(ApplySpikeDamageCoroutine());
    } */

    /*IEnumerator ApplySpikeDamageCoroutine()
    {
        HeroController.SharedInstance.TakeDamage(damagePoints);



        if (HeroController.SharedInstance.Health > 0)
        {
            HeroController.SharedInstance.UpdatePosition(HeroController.SharedInstance.LastPositionOnGround);
        }
    }*/
}
