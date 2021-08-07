using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 movementDirection;

    private Rigidbody2D _rigidbody;

    private static PlayerMovement _sharedInstance;

    public static PlayerMovement Instance
    {
        get
        {
            if (_sharedInstance == null)
            {
                _sharedInstance = FindObjectOfType<PlayerMovement>();
            }
            return _sharedInstance;
        }
        set
        {
            _sharedInstance = value;
        }
    }

    public Vector2 MovementDirection
    {
        get { return movementDirection; }
        set { movementDirection = value; }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Player.Instance.isTalking)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        if (!Player.Instance.isDead && Player.Instance.isControlable)
        {
            HandleMovement();
        }
    }

    /// <summary>
    /// Se encarga de manejar el movimiento en el eje X del personaje
    /// </summary>
    private void HandleMovement()
    {
        if (!Player.Instance.canMove) return;

        _rigidbody.velocity = new Vector2(movementDirection.x * speed, _rigidbody.velocity.y);

        if (Player.Instance.isOnGround)
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > 0) // se está moviendo?
            {
                // AQUI SE DEBERÍA AGREGAR UNA ANIMACION y COMPORTAMIENTO DE CUANDO ESTA ATACANDO
                // Y CORRIENDO A LA MISMA VEZ
                if (!Player.Instance.isAttacking) AnimatorController.Instance.Play(GlobalAnimID.Run.ToString());
            }
            else
            {
                if (!Player.Instance.isAttacking) AnimatorController.Instance.Play(GlobalAnimID.Idle.ToString());
            }
        }
        else if (Player.Instance.isFalling && !Player.Instance.isAttacking)
        {
            AnimatorController.Instance.Play(GlobalAnimID.Jump.ToString());
        }
    }
}
