using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private Animator _animator;
     //Limites a los que llegara el enemigo dependiendo el radio del collider circulo que se usa como referencia.
    float leftLimit;
    float rightLimit;

    [SerializeField]
    private float patrolSpeed = 1.25f;

    //Valor que usaremos para ayudarnos a saber a que direccion debe ir el enemigo
    private int direction = 1;

    //Definir enumeracion para los diferentes tipos de comportamientos de los enemigos
    enum EnemiesBehaviour {pasive, chasing, attacking}

    //Comportamiento por defecto sera pasivo
    EnemiesBehaviour behaviour = EnemiesBehaviour.pasive;

    //Definir la distancia a la que el zombie nos comienza a perseguir, a la que deja de perseguirnos y a la que nos ataca.
    [SerializeField]
    private float entryActiveZone = 2.5f;

    [SerializeField]
    private float exitActiveZone = 3f;

    [SerializeField]
    private float attackDistance = 0.35f;

    //Definir distancia entre enemigo y el jugador
    private float distanceEnemyPlayer;

    public Transform Player;

    private void Start()
    {
        //buscar el componente rigidbody2d
        _rigidbody = GetComponent<Rigidbody2D>();

        //calculos

        //limite izquierdo =  posicion actual de x - el radio hacia la izquierda.
        leftLimit = transform.position.x - GetComponent<CircleCollider2D>().radius;
        //limite derecho = posicion actual de x + el radio hacia la derecha.
        rightLimit = transform.position.x + GetComponent<CircleCollider2D>().radius;

        //encontrar la posicion del player
        Player = GameObject.Find("Player").transform;

        //buscar el componente animator
        _animator = transform.GetComponent<Animator>();

    }

    // Update is called once per frame
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
                    break;
                }
            case EnemiesBehaviour.chasing:
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
                    break;
                }
            case EnemiesBehaviour.attacking:
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
                    break;
                }
        }
    }
}