using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
