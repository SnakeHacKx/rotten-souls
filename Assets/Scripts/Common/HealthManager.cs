using UnityEngine;

/// <summary>
/// <para>Mánager de la Vida.</para>
/// Contiene todos los métodos referentes a la vida de los personajes (jugador o enemigos)
/// <list type="bullet">
/// <item>
/// <term>DamageCharacter</term>
/// <description>Controla el daño hacia el personaje</description>
/// </item>
/// <item>
/// <term>UpgradeMaxHealth</term>
/// <description>Actualiza la vida máxima</description>
/// </item>
/// </list>
/// </summary>
public class HealthManager : MonoBehaviour
{
    [Tooltip("Vida máxima")]
    [SerializeField] private int maxHealth;

    [Tooltip("Vida actual")]
    [SerializeField] private int currentHealth;

    /// <value>Devuelve el valor de la vida máxima del personaje</value>
    public int MaxHealth { get { return maxHealth; } }

    /// <value>Devuelve el valor de la vida actual del personaje</value>
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

    /// <summary>
    /// Controla el daño recibido
    /// </summary>
    /// <remarks>
    /// <para>Reproduce el sonido que indica que nos han golpeado, seguido, nos baja la vida según
    /// lo recibido por el parámetro <paramref name="damage"/>.</para>
    /// Para terminar, se evalúa si la vida actual es menor que cero, si es así, se desactiva al
    /// personaje.
    /// </remarks>
    /// <param name="damage">Cantidad de daño que recibe el personaje al ser golpeado.</param>
    public void DamageCharacter(int damage)
    {
        SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.HIT);

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Permite subirle la vida máxima al personaje
    /// al conseguir un objeto y/o una habilidad
    /// </summary>
    /// <param name="newMaxHealth">Nueva vida máxima a que se le quiere implementar al personaje</param>
    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        CurrentHealth = maxHealth;
    }
}