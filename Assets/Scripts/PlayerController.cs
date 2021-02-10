using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(LayerMask))]
[RequireComponent(typeof(Transform))]
public class PlayerController : MonoBehaviour
{
    // Permite saber a todo el programa que el jugador ya ha sido creado, esto
    // para que al cargar más de una escena, no se creen clones de este
    public static bool playerCreated;

    public bool isTalking;

    [SerializeField]
    [Tooltip("Tiempo que tardará en hacer la animación del Long Iddle")]
    private float longIdleTime = 5f;

    [SerializeField]
    [Tooltip("Velocidad de movimiento en el eje X")]
    private float speed = 2.5f;
    
    [SerializeField]
    [Tooltip("Fuerza de impulso al saltar")]
    private float jumpForce = 2.5f;

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
        // Is Grounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Is Jumping?
        if (Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(jumpTimeCounter > 0 && isJumping == true)
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

        // Wanna Attack?
        if (Input.GetKeyDown(KeyCode.Mouse0) && _isAttacking == false /*&& attacksOnAir <= maxAttacksOnAir*/)
        {
            _movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("Attacking");
        }
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
            // Movement
            float horizontalInput = Input.GetAxisRaw(AXIS_H);
            _movement = new Vector2(horizontalInput, 0f);

            // Flip character
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
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}