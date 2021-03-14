using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Tooltip("Vida máxima")]
    [SerializeField] private int maxHealth;
    
    [Tooltip("Vida actual")]
    [SerializeField] private int currentHealth;

    public int MaxHealth { get { return maxHealth; } }

    public int CurrentHealth
    {
        get { return currentHealth; }

        set
        {
            if (value < 0) currentHealth = 0;

            else currentHealth = value;
        }
    }

    private void Start()
    {
        // Al iniciar el juego la vida la ponemos al máximo
        UpdateMaxHealth(maxHealth);
    }

    public void DamageCharacter(int damage)
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.HIT);

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // si quiero subirle la vida máxima al jugador/enemigo
    // al conseguir un objeto o habilidad
    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        CurrentHealth = maxHealth;
    }
}