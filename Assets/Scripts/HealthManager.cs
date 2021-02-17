using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Vida máxima")]
    private int maxHealth;

    [SerializeField]
    [Tooltip("Vida actual")]
    private int currentHealth;
    
    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth { get { return currentHealth; } }

    // Start is called before the first frame update
    void Start()
    {
        // Al iniciar el juego la vida la ponemos al máximo
        UpdateMaxHealth(maxHealth);
    }

    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // si quiero subirle la vida máxima al jugador/enemigo
    // al conseguir un objeto o habilidad
    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }
}
