using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
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
    //[SerializeField]
    //[Tooltip("Número de ataques máximo que puede hacer el player en el aire")]
    //private int maxAttacksOnAir = 1;
    //private int attacksOnAir;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // attacksOnAir = 0;
    }

    private void Update()
    {
        /*if (_isAttacking == false) {
			// Movement
			float horizontalInput = Input.GetAxisRaw("Horizontal");
			_movement = new Vector2(horizontalInput, 0f);

			// Flip character
			if (horizontalInput < 0f && _facingRight == true) {
				Flip();
			} else if (horizontalInput > 0f && _facingRight == false) {
				Flip();
			}
		}*/

        // Is Grounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Is Jumping?
        if (Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //attacksOnAir = 0;
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
            _animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        if (_isAttacking == false)
        {
            float horizontalVelocity = _movement.normalized.x * speed;
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        }

        if (_isAttacking == false)
        {
            // Movement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
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
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

        // Animator
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
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