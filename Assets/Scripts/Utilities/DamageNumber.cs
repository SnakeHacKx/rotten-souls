using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// <para>[ESTA CLASE NO ESTÁ EN USO]</para>
/// Hace salir unos números en pantalla que indican el daño causado por el jugador al enemigo
/// </summary>
public class DamageNumber : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Velocidad con la cual se moverá el texto")]
    private float damageSpeed;

    [SerializeField]
    //[Tooltip("Puntos")]
    private float damagePoints;

    [SerializeField]
    private TMP_Text damageText;

    public float GetDamagePoints { get { return damagePoints; } }
    public float SetDamagePoints { set { damagePoints = value; } }

    void Update()
    {
        damageText.text = damagePoints.ToString();

        this.transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y + damageSpeed * Time.deltaTime,
            this.transform.position.z);

        //this.transform.localScale = this.transform.localScale * (1 - Time.deltaTime);
        damageText.alpha *= (1 - Time.deltaTime * 3);

    }
}
