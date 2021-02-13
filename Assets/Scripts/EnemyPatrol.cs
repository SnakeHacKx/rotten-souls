using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    //Limites a los que llegara el enemigo dependiendo el radio del collider circulo que se usa como referencia.
    private float leftLimit;

    private float rightLimit;

    [SerializeField]
    private float patrolSpeed = 1.25f;

    //Valor que usaremos para ayudarnos a saber a que direccion debe ir el enemigo
    private int direction = 1;

    //Definir enumeracion para los diferentes tipos de comportamientos de los enemigos
    private enum EnemiesBehaviour
    { pasive, chasing, attacking }

    private EnemiesBehaviour behaviour = EnemiesBehaviour.pasive;

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
        _rigidbody = GetComponent<Rigidbody2D>();

        //calculos

        //limite izquierdo =  posicion actual de x - el radio hacia la izquierda.
        leftLimit = transform.position.x - GetComponent<CircleCollider2D>().radius;
        //limite derecho = posicion actual de x + el radio hacia la derecha.
        rightLimit = transform.position.x + GetComponent<CircleCollider2D>().radius;

        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        float playerPosition;
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
        distanceEnemyPlayer = Mathf.Abs(playerPosition - transform.position.x);
        switch (behaviour)
        {
            case EnemiesBehaviour.pasive:
                {
                    //la velocidad del enemigo en x sera igual a la velocidad de parulla (patrolSpeed) * la direccion a la cual tiene que ir. (la velocidad de y se mantiene igual)
                    _rigidbody.velocity = new Vector2(patrolSpeed * direction, _rigidbody.velocity.y);

                    //si la posicion actual de x es menor que el limite izquierdo
                    if (transform.position.x < leftLimit)
                    {
                        //direccion del personaje cambia a 1
                        direction = 1;
                        //usar el componente sprite renderer para flipear el sprite
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    //si la posicion actual del personaje es mayor que el limite derecho
                    if (transform.position.x > rightLimit)
                    {
                        //direccion del personaje cambia a izquierda.
                        direction = -1;
                        //usar el componente sprite renderer para flipear el sprite
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    }

                    if (distanceEnemyPlayer < entryActiveZone) behaviour = EnemiesBehaviour.chasing;
                    break;
                }
            case EnemiesBehaviour.chasing:
                {
                    //la velocidad del enemigo en x sera igual a la velocidad de parulla (patrolSpeed) * la direccion a la cual tiene que ir. (la velocidad de y se mantiene igual)
                    _rigidbody.velocity = new Vector2(patrolSpeed * 1.5f * direction, _rigidbody.velocity.y);

                    //si la posicion actual de x es menor que el limite izquierdo
                    if (playerPosition > transform.position.x)
                    {
                        //direccion del personaje cambia a 1
                        direction = 1;
                        //usar el componente sprite renderer para flipear el sprite
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    //si la posicion actual del personaje es mayor que el limite derecho
                    if (playerPosition < transform.position.x)
                    {
                        //direccion del personaje cambia a izquierda.
                        direction = -1;
                        //usar el componente sprite renderer para flipear el sprite
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    if (distanceEnemyPlayer > exitActiveZone) behaviour = EnemiesBehaviour.pasive;
                    if (distanceEnemyPlayer < attackDistance) behaviour = EnemiesBehaviour.attacking;
                    break;
                }
            case EnemiesBehaviour.attacking:
                {
                    //si la posicion actual de x es menor que el limite izquierdo
                    if (playerPosition > transform.position.x)
                    {
                        //direccion del personaje cambia a 1
                        direction = 1;
                        //usar el componente sprite renderer para flipear el sprite
                        gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    //si la posicion actual del personaje es mayor que el limite derecho
                    if (playerPosition < transform.position.x)
                    {
                        //direccion del personaje cambia a izquierda.
                        direction = -1;
                        //usar el componente sprite renderer para flipear el sprite
                        gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    if (distanceEnemyPlayer > attackDistance) behaviour = EnemiesBehaviour.chasing;
                    break;
                }
        }
    }
}