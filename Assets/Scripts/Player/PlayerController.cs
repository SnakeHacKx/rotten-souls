﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(LayerMask))]

public class PlayerController : MonoBehaviour
{
    // Permite saber a todo el programa que el jugador ya ha sido creado, esto
    // para que al cargar más de una escena, no se creen clones de este
    public static bool playerCreated;

    public bool isTalking;
    public bool isWalking = false;

    [Tooltip("Tiempo que tardará en hacer la animación del Long Iddle")]
    [SerializeField] private float longIdleTime = 5f;
    
    [Header("Rigid Variables")]
    [Tooltip("Velocidad de movimiento en el eje X")]
    [SerializeField] private float speed = 2.5f;
    
    [Tooltip("Fuerza de impulso al saltar")]
    [SerializeField] private float jumpForce = 2.5f;

    //[Header("Audio Variables")]
    //[SerializeField] private AudioSource _walkingStepSound;
    //[SerializeField] private AudioSource _attackingSound;

    //[SerializeField] private AudioManager audioManager;
 
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    // References
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    // Long Idle
    private float _longIdleTimer;

    // Movement
    private Vector2 _movement;
    private bool _facingRight = true;

    // Jump
    private bool _isGrounded;
    private float jumpTimeCounter;

    [SerializeField]
    [Tooltip("Tiempo máximo en el que será válido que el usuario presione el botón de saltar")]
    private float jumpTime;
    private bool isJumping;

    // Attack
    private bool _isAttacking;

    public Vector2 lastMovement = Vector2.zero;
    private const string JUMP = "Jump";
    private const string ATTACK = "Attack";
    private const string AXIS_H = "Horizontal", AXIS_V = "VerticalVelocity";

    // guarda la referencia del siguiente lugar al que se quiere ir
    public string nextUuid;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Si no se ha creado el jugador, "se crea"
        if (!playerCreated)
        {
            playerCreated = true;
        }

        isTalking = false;
    }

    private void Update()
    {
        // Está en el suelo?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        HandleJump();
        HandleAttack();
        //HandleMovement();
    }

    private void FixedUpdate()
    {
        if (isTalking)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        if (_isAttacking == false)
        {
            float horizontalVelocity = _movement.normalized.x * speed;
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        }

        if (_isAttacking == false)
        {
            // Movimiento
            float horizontalInput = Input.GetAxisRaw(AXIS_H);
            _movement = new Vector2(horizontalInput, 0f);

            // Girar personaje
            if (horizontalInput < 0f && _facingRight == true)
            {
                Flip();
            }
            else if (horizontalInput > 0f && _facingRight == false)
            {
                Flip();
            }
        }
           
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat(AXIS_V, _rigidbody.velocity.y);
        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));

        // Animator
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attacking"))
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }

        // Long Idle
        if (_movement == Vector2.zero && _isGrounded == true)
        {
            _longIdleTimer += Time.deltaTime;

            if (_longIdleTimer >= longIdleTime)
            {
                _animator.SetTrigger("LongIdle");
            }
        }
        else
        {
            _longIdleTimer = 0f;
        }

        if (_isGrounded && _movement == Vector2.zero)
        {
            StopStepSound();
        }
    }

    public void PlayStepSound()
    {
        //_walkingStepSound.Play();
    }

    private void StopStepSound()
    {
        //_walkingStepSound.Stop();
    }

    private void HandleMovement()
    {
        // Está caminando?
        /*if (_isGrounded && _movement != Vector2.zero)
        {
            //if (!_walkingStepSound.isPlaying)
            //    _walkingStepSound.Play();
            PlayStepSound();

        }*/
        //else
        //{
        //    //_walkingStepSound.Stop();
        //}
    }

    private void HandleAttack()
    {
        // Quieres atacar?
        if (Input.GetButtonDown(ATTACK) && _isAttacking == false)
        {
            _movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("Attacking");
            //SFXManager.SharedInstance.PlaySFX(SFXType.SoundType.ATTACK);
        }
    }

    private void HandleJump()
    {
        // Está saltando?
        if (Input.GetButtonDown(JUMP) && _isGrounded == true && _isAttacking == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick2Button0))
        {
            if (jumpTimeCounter > 0 && isJumping == true)
            {
                _rigidbody.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}