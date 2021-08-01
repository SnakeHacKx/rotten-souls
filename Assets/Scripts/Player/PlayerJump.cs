using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;

    private Rigidbody2D _rigidbody;

    [Tooltip("Tiempo máximo en el que será válido que el usuario presione el botón de saltar")]
    [SerializeField] private float jumpTime;
    private float jumpTimeCounter;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!Player.Instance.isDead && Player.Instance.isControlable)
        {
            HandleJump();
        }
    }

    /// <summary>
    /// Se encarga de manejar el salto del personaje y sus diferentes variaciones.
    /// </summary>
    private void HandleJump()
    {
        // Doble Salto
        if (Player.Instance.canDoubleJump && InputManager.Instance.JumpButtonPressed && !Player.Instance.isOnGround)
        {
            this._rigidbody.velocity = Vector2.zero;
            this._rigidbody.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            Player.Instance.canDoubleJump = false;
        }

        // Salto normal
        if (InputManager.Instance.JumpButtonPressed && Player.Instance.isOnGround)
        {
            Player.Instance.isJumping = true;

            jumpTimeCounter = jumpTime;

            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Comienza la corrutina del salto
            //todo:avtivar corrutina de salto cuando tenga la animacion
            StartCoroutine(HandleJumpAnimation());

            Player.Instance.canDoubleJump = true;
        }

        if (jumpTimeCounter > 0 && Player.Instance.isJumping == true)
        {
            _rigidbody.velocity = Vector2.up * jumpForce;
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            Player.Instance.isJumping = false;
        }

        if (Input.GetButtonUp("Jump"))
        {
            Player.Instance.isJumping = false;
        }
    }

    /// <summary>
    /// Corrutina del salto.
    /// </summary>
    /// <returns></returns>
    IEnumerator HandleJumpAnimation()
    {
        Player.Instance.canCheckGround = false;
        Player.Instance.isOnGround = false;
        Player.Instance.isFalling = false;

        if (!Player.Instance.isAttacking)
            AnimatorController.Instance.Play(AnimationID.PrepareJump);

        yield return new WaitForSeconds(0.1f); // retraso de 0.3 segundos

        if (!Player.Instance.isAttacking)
            AnimatorController.Instance.Play(AnimationID.Jump);

        Player.Instance.canCheckGround = true;
    }
}
