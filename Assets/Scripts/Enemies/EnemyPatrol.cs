using UnityEngine;

/// <summary>
/// Controla el patrullaje que hacen los enemigos.
/// <list type="bullet">
/// <item>
/// <term>Start</term>
/// <description>...</description>
/// </item>
/// <item>
/// <term>Update</term>
/// <description>...</description>
/// </item>
/// </list>
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    // Referencia al componente rigidbody
    private Rigidbody2D _rigidbody;

    // Referencia al componente animator
    private Animator _animator;

    //Límites a los que llegará el enemigo dependiendo el radio del CircleCollider que se usa como referencia.
    private float leftLimit;

    private float rightLimit;

    [Tooltip("Velocidad con la que el enemigo patrullará")]
    [SerializeField] private float patrolSpeed = 1.25f;

    // Valor que usaremos para ayudarnos a saber a que direccion debe ir el enemigo
    private int direction = 1;

    // Define la enumeración para los diferentes tipos de comportamientos de los enemigos
    private enum EnemiesBehaviour
    { pasive, chasing, attacking }

    // Comportamiento por defecto sera pasivo
    private EnemiesBehaviour behaviour = EnemiesBehaviour.pasive;

    // Definir la distancia a la que el zombie nos comienza a perseguir, a la que deja de perseguirnos y a la que nos ataca.
    [SerializeField] private float entryActiveZone = 2.5f;

    [SerializeField] private float exitActiveZone = 3f;

    [SerializeField] private float attackDistance = 0.35f;

    // Define la distancia entre enemigo y el jugador
    private float distanceEnemyPlayer;

    // Referencia al transform del jugador
    public Transform player;

    private void Start()
    {
        //buscar el componente rigidbody2d
        _rigidbody = GetComponent<Rigidbody2D>();

        //calculos

        //limite izquierdo =  posicion actual de x - el radio hacia la izquierda.
        leftLimit = transform.position.x - GetComponentInChildren<CircleCollider2D>().radius;
        //limite derecho = posicion actual de x + el radio hacia la derecha.
        rightLimit = transform.position.x + GetComponentInChildren<CircleCollider2D>().radius;

        //encontrar la posicion del player
        player = GameObject.Find("Player").transform;

        //buscar el componente animator
        _animator = transform.GetComponent<Animator>();
    }

    /// <summary>
    /// ...
    /// </summary>
    private void Update()
    {
        float playerPosition;

        //posicion del jugador = a la posicion x del objeto con el tag player
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;

        //distancia entre el enemigo y el player = valor absoluto de la posicion del player - la posicion del enemigo
        distanceEnemyPlayer = Mathf.Abs(playerPosition - transform.position.x);

        switch (behaviour)
        {
            case EnemiesBehaviour.pasive:
                {
                    PasiveState();
                    break;
                }
            case EnemiesBehaviour.chasing:
                {
                    ChasingState(playerPosition);
                    break;
                }
            case EnemiesBehaviour.attacking:
                {
                    AttackingState(playerPosition);
                    break;
                }
        }
    }

    /// <summary>
    /// ...
    /// </summary>
    private void PasiveState()
    {
        //hacer que la animacion del enemigo sea caminar
        _animator.SetBool("Walk", true);

        //la velocidad del enemigo en x sera igual a la velocidad de parulla (patrolSpeed) * la direccion a la cual tiene que ir. (la velocidad de y se mantiene igual)
        _rigidbody.velocity = new Vector2(patrolSpeed * direction, _rigidbody.velocity.y);

        //si la posicion actual de x es menor que el limite izquierdo
        if (transform.position.x < leftLimit)
        {
            //direccion del personaje cambia a 1
            direction = 1;

            //velocidad del animator
            _animator.speed = 1f;

            //usar el componente sprite renderer para flipear el sprite
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        //si la posicion actual del personaje es mayor que el limite derecho
        if (transform.position.x > rightLimit)
        {
            //direccion del personaje cambia a izquierda.
            direction = -1;

            //velocidad del animator
            _animator.speed = 1f;

            //usar el componente sprite renderer para flipear el sprite
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        //entrar a modo persecucion
        if (distanceEnemyPlayer < entryActiveZone) behaviour = EnemiesBehaviour.chasing;
    }

    /// <summary>
    /// ...
    /// </summary>
    /// <param name="playerPosition">Posición del jugador.</param>
    private void ChasingState(float playerPosition)
    {
        //hacer que la animacion del enemigo sea caminar
        _animator.SetBool("Walk", true);

        //la velocidad del enemigo en x sera igual a la velocidad de parulla (patrolSpeed) * la direccion a la cual tiene que ir. (la velocidad de y se mantiene igual)
        _rigidbody.velocity = new Vector2(patrolSpeed * 1.5f * direction, _rigidbody.velocity.y);

        //si la posicion actual de x es menor que el limite izquierdo
        if (playerPosition > transform.position.x)
        {
            //direccion del personaje cambia a 1
            direction = 1;

            //velocidad del animator
            _animator.speed = 2f;

            //usar el componente sprite renderer para flipear el sprite
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        //si la posicion actual del personaje es mayor que el limite derecho
        if (playerPosition < transform.position.x)
        {
            //direccion del personaje cambia a izquierda.
            direction = -1;

            //velocidad del animator
            _animator.speed = 2f;

            //usar el componente sprite renderer para flipear el sprite
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        //entrar a modo pasivo
        if (distanceEnemyPlayer > exitActiveZone) behaviour = EnemiesBehaviour.pasive;

        //atacar
        if (distanceEnemyPlayer < attackDistance) behaviour = EnemiesBehaviour.attacking;

    }

    /// <summary>
    /// ...
    /// </summary>
    /// <param name="playerPosition">Posición del jugador.</param>
    private void AttackingState(float playerPosition)
    {
        //hacer que el enemigo haga la animacion de ataque
        _animator.SetTrigger("Attack");

        //si la posicion actual de x es menor que el limite izquierdo
        if (playerPosition > transform.position.x)
        {
            //direccion del personaje cambia a 1
            direction = 1;

            //velocidad del animator
            _animator.speed = 1f;

            //usar el componente sprite renderer para flipear el sprite
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        //si la posicion actual del personaje es mayor que el limite derecho
        if (playerPosition < transform.position.x)
        {
            //direccion del personaje cambia a izquierda.
            direction = -1;

            //velocidad del animator
            _animator.speed = 1f;

            //usar el componente sprite renderer para flipear el sprite
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        //volver a modo persecucion
        if (distanceEnemyPlayer > attackDistance) behaviour = EnemiesBehaviour.chasing;
    }
}