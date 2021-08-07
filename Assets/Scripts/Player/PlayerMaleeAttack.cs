using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaleeAttack : MonoBehaviour
{
    [Header("Private Variables")]
    [SerializeField] private SwordController _swordController;
    [SerializeField] private float _delayAttack;
    [SerializeField] private float _attackDuration;

    [Header("Audio")]
    [SerializeField] private AudioClip attackSFX;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.Instance.isDead && Player.Instance.isControlable)
        {
            HandleAttack();
        }
    }

    /// <summary>
    /// Se encarga de manejar el ataque del jugador.
    /// </summary>
    private void HandleAttack()
    {
        if (InputManager.Instance.AttackButtonPressed && !Player.Instance.isAttacking)
        {
            if (Player.Instance.isOnGround) _rigidbody.velocity = Vector2.zero;

            AudioManager.SharedInstance.PlaySFX(attackSFX);
            AnimatorController.Instance.Play(GlobalAnimID.Attack.ToString());
            Player.Instance.isAttacking = true;
            _swordController.Attack(_delayAttack, _attackDuration);

            StartCoroutine(RestoreAttack());
        }
    }

    /// <summary>
    /// Corrutina del ataque, se extiende desde que el personaje tiene la espada en
    /// la distancia maxima del ataque, hasta que la esconde y vuelve al Idle
    /// </summary>
    IEnumerator RestoreAttack()
    {
        if (Player.Instance.isOnGround) Player.Instance.canMove = false;

        yield return new WaitForSeconds(0.35f); // El ataque durará 3s
        Player.Instance.isAttacking = false;

        Player.Instance.canMove = true;
    }
}
