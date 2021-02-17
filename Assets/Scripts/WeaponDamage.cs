using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public GameObject canvasDamage;
    public GameObject hitPoint;

    [SerializeField]
    [Tooltip("Daño que proporciona a los enemigos")]
    private int damage;

    private void Start()
    {
        hitPoint = transform.Find("Hit Point").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var clone = (GameObject)Instantiate(canvasDamage,
                hitPoint.transform.position,
                Quaternion.Euler(Vector3.zero));

            clone.GetComponent<DamageNumber>().SetDamagePoints = damage;

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }
}
