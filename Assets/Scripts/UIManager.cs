using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider playerHealthBar;

    [SerializeField]
    private TMP_Text playerHealthText;

    [SerializeField]
    private HealthManager playerHealthManager;

    void Update()
    {
        // ESTO SE PUEDE OPTIMIZAR
        // Esto se llama 60 veces por segundo, es ineficiente
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
