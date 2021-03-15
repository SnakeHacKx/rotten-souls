using UnityEngine;

/// <summary>
/// Controla el daño de las armas.
/// </summary>
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

    /// <summary>
    /// Si el arma entra en colisión con un enemigo, esta le genera un daño.
    /// </summary>
    /// <param name="collision">Guarda los datos de la colisión.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagID.Enemy.ToString()))
        {
            Debug.Log("Golpeó a un enemigo");
            var clone = (GameObject)Instantiate(
                canvasDamage,
                hitPoint.transform.position,
                Quaternion.Euler(Vector3.zero));

            clone.GetComponent<DamageNumber>().SetDamagePoints = damage;

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }
}