using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public GameObject canvasDamage;
    public GameObject hitPoint;
    
    [Tooltip("Daño que proporciona a los enemigos")]
    [SerializeField] private int damage;

    private void Start()
    {
        hitPoint = transform.Find("Hit Point").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Golpeó a un enemigo");
            var clone = (GameObject)Instantiate(canvasDamage,
                hitPoint.transform.position,
                Quaternion.Euler(Vector3.zero));

            clone.GetComponent<DamageNumber>().SetDamagePoints = damage;

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }
}