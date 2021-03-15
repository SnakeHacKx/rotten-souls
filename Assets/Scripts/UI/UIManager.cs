using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

/// <summary>
/// <para>Mánager de la Interfaz de Usuaio.</para>
/// Contiene todos los métodos referentes a la vinterfaz gráfica de usuario (GUI).
/// <list type="bullet">
/// <item>
/// <term>Update</term>
/// <description>Actualiza la vida en el HUD.</description>
/// </item>
/// </list>
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider playerHealthBar;

    [SerializeField] private TMP_Text playerHealthText;

    [SerializeField] private HealthManager playerHealthManager;

    /// <summary>
    /// Actualiza la vida que ve el usuario en el HUD.
    /// <para>[SE DEBE OPTIMIZAR]</para>
    /// </summary>
    void Update()
    {
        // ESTO SE DEBE OPTIMIZAR
        // Estas líneas se llaman 60 veces por segundo, es ineficiente
        // Sólo tendriamos que llamar a eso cuando nos hacemos daño o ganamos vida
        // La forma perfecta sería usando eventos y delegados

        playerHealthBar.maxValue = playerHealthManager.MaxHealth;
        playerHealthBar.value = playerHealthManager.CurrentHealth;

        StringBuilder stringBuilder = new StringBuilder()
            .Append("HP: ")
            .Append(playerHealthManager.CurrentHealth)
            .Append(" / ")
            .Append(playerHealthManager.MaxHealth);

        playerHealthText.text = stringBuilder.ToString();
    }
}
