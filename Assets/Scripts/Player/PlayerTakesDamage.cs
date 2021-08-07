using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakesDamage : MonoBehaviour
{
    [SerializeField] private float damageForce; // fuerza de repulsión cuando cuando el jugador es golpeado
    [SerializeField] private float damageForceUp;
    [SerializeField] private float spikeDamageForceUp;

    [SerializeField] private DamageFeedbackEffect _damageFeedbackEffect;

    private static PlayerTakesDamage _sharedInstance;

    public static PlayerTakesDamage Instance
    {
        get {  return _sharedInstance; }
    }

    private Rigidbody2D _rigidbody;
 
    private void Start()  
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damagePoints)
    {
        if (!Player.Instance.isRecovering && !Player.Instance.isDead && !Player.Instance.isTakingDamage)
        {
            Player.Instance.isTakingDamage = true;
            // Clamp evita que se haga negativa la variable
            HealthManager.Instance.Health = Mathf.Clamp(HealthManager.Instance.Health - damagePoints, 0, HealthManager.Instance.MaxHealth);

            if (HealthManager.Instance.Health <= 0)
            {
                Player.Instance.isDead = true;
                Player.Instance.SetIsControlable(false);
                _rigidbody.velocity = Vector2.zero;

                // Aqui debería llamar a alguna corrutina de muerte

                if (GameManager.SharedInstance.lastCheckpointScene == 0)
                {
                    Debug.Log("EL jugador ha muerto, reiniciando desde puntod de control...");
                    StopAllCoroutines();
                    SceneHelper.SharedInstance.RestartGame();
                    return;
                }

                SceneHelper.SharedInstance.GoToLastCheckPoint();
            }

            StartCoroutine(StartPlayerRecover());
            DamageImpulse();

            Player.Instance.isTakingDamage = false;
        }
    }

    public void TakeSpikeDamage(int damagePoints)
    {
        if (!Player.Instance.isRecovering && !Player.Instance.isDead && !Player.Instance.isTakingDamage)
        {
            Debug.Log("El jugador ha sufrido daño por espinas");
            Player.Instance.isTakingDamage = true;

            HealthManager.Instance.Health = Mathf.Clamp(HealthManager.Instance.Health - damagePoints, 0, HealthManager.Instance.MaxHealth);

            if (HealthManager.Instance.Health <= 0)
            {
                Player.Instance.isDead = true;
                Player.Instance.SetIsControlable(false);
                _rigidbody.velocity = Vector2.zero;
                // todo: Hacer una corrutina de muerte

                if (GameManager.SharedInstance.lastCheckpointScene == 0)
                {
                    Debug.Log("EL jugador ha muerto, reiniciando el juego");
                    StopAllCoroutines();
                    SceneHelper.SharedInstance.RestartGame();
                    return;
                }

                SceneHelper.SharedInstance.GoToLastCheckPoint();
                Debug.Log("EL jugador ha muerto, reiniciando desde punto de control...");
                return;
            }
            PlayerTakeSpikeDamage();

            Player.Instance.isTakingDamage = false;
            Debug.Log("El jugador puede recibir daño");

            FindObjectOfType<SpikeDamageTeleport>().SendPlayerToRespawnPosition();
        }
    }

    void DamageImpulse()
    {
        if (Player.Instance.isLookingRight)
        {
            _rigidbody.AddForce(Vector2.left * damageForce + Vector2.up * damageForceUp, ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody.AddForce(Vector2.right * damageForce + Vector2.up * damageForceUp, ForceMode2D.Impulse);
        }
    }

    void SpikeDamageImpulse()
    {
        if (Player.Instance.isLookingRight)
        {
            _rigidbody.AddForce(Vector2.left * damageForce + Vector2.up * spikeDamageForceUp, ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody.AddForce(Vector2.right * damageForce + Vector2.up * spikeDamageForceUp, ForceMode2D.Impulse);
        }
    }

    public void PlayerTakeSpikeDamage()
    {
        StartCoroutine(PlayerTakeSpikeDamageCoroutine());
    }

    public IEnumerator PlayerTakeSpikeDamageCoroutine()
    {
        Player.Instance.canMove = false;
        Player.Instance.canFlip = false;
       
        AnimatorController.Instance.Play(GlobalAnimID.Hurt.ToString());
        SpikeDamageImpulse();
        yield return new WaitForSeconds(0.5f);
        AnimatorController.Instance.Play(GlobalAnimID.Idle.ToString());
        
        Player.Instance.canMove = true;
        Player.Instance.canFlip = true;
        //_rigidbody.velocity = Vector2.zero;
        Player.Instance.isRecovering = true;
        Debug.Log("El jugador ha comenzado a recuperarse");

        _damageFeedbackEffect.PlayBlinkDamageEffect();
        yield return new WaitForSeconds(2f);
        _damageFeedbackEffect.StopBlinkDamageEffect();

        Player.Instance.isRecovering = false;
        Debug.Log("El jugador ha terminado de recuperarse");

    }

    IEnumerator StartPlayerRecover()
    {
        // Esto es más que todo para que el impulso tenga efecto (el que pasa cuando nos hacen daño) 
        Player.Instance.canMove = false;
        Player.Instance.canFlip = false;

        AnimatorController.Instance.Play(GlobalAnimID.Hurt.ToString());
        yield return new WaitForSeconds(0.2f);

        Player.Instance.canMove = true;
        Player.Instance.canFlip = true;
        _rigidbody.velocity = Vector2.zero;

        Player.Instance.isRecovering = true;
        _damageFeedbackEffect.PlayBlinkDamageEffect();
        yield return new WaitForSeconds(2);

        _damageFeedbackEffect.StopBlinkDamageEffect();
        Player.Instance.isRecovering = false;
    }
}
