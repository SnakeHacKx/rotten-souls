using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool jumpPressed = false;
    private bool attackPressed = false;
    private bool usePowerUpPressed = false;

    public bool JumpButtonPressed { get { return jumpPressed; } }
    public bool AttackButtonPressed { get { return attackPressed; } }
    public bool UsePowerUpButtonPressed { get { return usePowerUpPressed; } }

    private static InputManager _sharedInstance;

    public static InputManager Instance
    {
        get
        {
            if (_sharedInstance == null)
            {
                _sharedInstance = FindObjectOfType<InputManager>();
            }
            return _sharedInstance;
        }
        set
        {
            _sharedInstance = value;
        }
    }

    private void Update()
    {
        if (!Player.Instance.isDead && Player.Instance.isControlable)
        {
            HandleControls();
        }
    }

    private void HandleControls()
    {
        PlayerMovement.Instance.MovementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // GetButton vale sólamente para un fotograma
        // necesitamos el GetButtonDown para que sea mientras presionamos la tecla
        jumpPressed = Input.GetButtonDown("Jump"); // Por default es la barra espaciadora
        attackPressed = Input.GetButtonDown("Attack");
        usePowerUpPressed = Input.GetButtonDown("UsePowerUp");
    }
}
