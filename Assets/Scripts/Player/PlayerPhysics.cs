using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [Header("Checker Variables")]
    [SerializeField] private LayerChecker _footA;
    [SerializeField] private LayerChecker _footB;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!Player.Instance.isDead && Player.Instance.isControlable)
        {
            HandleIsGrounding();
            HandleFalling();
            HandleFlip();
        }
    }

    private void HandleIsGrounding()
    {
        if (!Player.Instance.canCheckGround) return;

        Player.Instance.isOnGround = _footA.isTouching || _footB.isTouching;
    }

    /// <summary>
    /// Se encarga de manejar el giro del jugador.
    /// </summary>
    private void HandleFlip()
    {
        if (!Player.Instance.canFlip) return;

        if (_rigidbody.velocity.magnitude > 0)
        {
            if (_rigidbody.velocity.x > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                Player.Instance.isLookingRight = true;
            }
            else if (_rigidbody.velocity.x < 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
                Player.Instance.isLookingRight = false;
            }
        }
    }

    private void HandleFalling()
    {
        if (!Player.Instance.isOnGround && !Player.Instance.isJumping)
        {
            Player.Instance.isFalling = true;
        }

        if (Player.Instance.isOnGround || Player.Instance.isJumping)
        {
            Player.Instance.isFalling = false;
        }
    }
}
