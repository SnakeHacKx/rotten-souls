using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void _PlayerExists();
    public static event _PlayerExists PlayerExists;

    [Header("Power Ups")]
    [SerializeField] private PowerUpID _currentPowerUp;
    [SerializeField] private int _powerUpAmount;

    [Header("Interruption Variables")]
    public bool canCheckGround;
    public bool canMove;
    public bool canFlip;
    public bool canDoubleJump;

    [Header("Boolean Variables")]
    // TODO: cambiar estas variables a private
    public bool isAttacking;
    public bool isRecovering;
    public bool isLookingRight;
    public bool isUsingPowerUp;
    public bool isFalling;
    public bool isOnGround;
    public bool isJumping;
    public bool isDead;
    public bool isControlable = true;
    public bool isTakingDamage = false;
    public bool isTalking;

    private Vector3 lastPositionOnGround;

    private int _coins = -1;

    [Header("Animation Variables")]
    [SerializeField] private AnimatorController _animatorController;

    // Referencias a otras clases
    private GameData _gameData;
    private LevelManager _levelManager;
    private SpikeSurroundings spikesSurroundings;
    private Rigidbody2D _rigidbody;
    private HealthManager _healthManager;

    // Permite saber a todo el programa que el jugador ya ha sido creado, esto
    // para que al cargar más de una escena, no se creen clones de este
    public static bool playerCreated;

    // Singleton
    private static Player _sharedInstance;
    public static Player Instance
    {
        get
        {
            if (_sharedInstance == null)
            {
                _sharedInstance = FindObjectOfType<Player>();
            }
            return _sharedInstance;
        }
        set
        {
            _sharedInstance = value;
        }
    }

    // Propiedades
    public int Coins
    {
        get
        {
            return _coins;
        }
        set
        {
            if (_coins != value)
            {
                GameManager.SharedInstance.UpdateCoins(value);
            }
            _coins = value;
        }
    }

    public Vector3 LastPositionOnGround { get { return lastPositionOnGround; } set { lastPositionOnGround = value; } }

    public int PowerUpAmount
    {
        get
        {
            return _powerUpAmount;
        }
        set
        {
            if (_powerUpAmount != value)
            {
                //print("Debio actualizarse el numero de power ups");
                GameManager.SharedInstance.UpdatePowerUp(value);
            }
            _powerUpAmount = value;
        }
    }

    public PowerUpID CurrentPowerUpID
    {
        get
        {
            return _currentPowerUp;
        }
        set
        {
            _currentPowerUp = value;
        }
    }

    private void Awake()
    {
        if (!playerCreated)
        {
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        // Inicializacion de componentes
        _levelManager = GetComponent<LevelManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
        spikesSurroundings = FindObjectOfType<SpikeSurroundings>();

        // Si no se ha creado el jugador, "se crea"
        if (!playerCreated) playerCreated = true;

        // Inicializacion de variables del jugador
        isTalking = false;
        canCheckGround = true;
        canMove = true;
        canFlip = true;
        
        // Animacion "Idle" al iniciar
        _animatorController.Play(AnimationID.Idle);

        PlayerExists?.Invoke();
    }

    /// <summary>
    /// Carga el ultimo estado guardado del jugador.
    /// </summary>
    public void LoadPlayerStatus()
    {
        if (_gameData == null)
            return;

        Debug.Log("La vida del jugador en la BD es: " + _gameData.heroData.health.ToString());
        // TODO: probar si esto de llamar al health manager me devuelve la vida del jugador
        _healthManager.Health = _gameData.heroData.health;
        Coins = _gameData.heroData.coinsAmount;
        PowerUpAmount = _gameData.heroData.powerUpAmount;
        _currentPowerUp = _gameData.heroData.currentPowerUpID;
    }

    /// <summary>
    /// Establece la animacion "Idle" para el jugador
    /// </summary>
    public void SetIdleAnimation()
    {
        _animatorController.Play(AnimationID.Idle);
    }

    /// <summary>
    /// Establece los valores iniciales del jugador al iniciar una nueva partida
    /// </summary>
    public void SetPlayerToNewGameStatus()
    {
        Invoke(nameof(SetOnNewGamePosition), 0.2f);
        Invoke(nameof(SetPlayerToDefaltAtributes), 0.3f);
        SetIsControlable(true);
        _healthManager.Health = _healthManager.InitialHealth;
        Coins = 0;
        PowerUpAmount = 0;
        _animatorController.Play(AnimationID.Idle);
        _currentPowerUp = PowerUpID.Nothing;
    }

    /// <summary>
    /// Establece los valores por defecto del jugador, por ejemplo, despues de morir
    /// </summary>
    public void SetPlayerToDefaultStatus()
    {
        Invoke(nameof(SetOnLastCheckpointPosition), 0.2f);
        Invoke(nameof(SetPlayerToDefaltAtributes), 0.3f);
        SetIsControlable(true);
        _healthManager.Health = _healthManager.MaxHealth;
        Coins = 0;
        PowerUpAmount = 0;
        _animatorController.Play(AnimationID.Idle);
        _currentPowerUp = PowerUpID.Nothing;
    }

    /// <summary>
    /// Todos los atributos y estados del jugador vuelven a su estado por defecto
    /// </summary>
    public void SetPlayerToDefaltAtributes()
    {
        isDead = false;
        isAttacking = false;
        isFalling = false;
        isRecovering = false;
        isTakingDamage = false;
        isUsingPowerUp = false;
        isTalking = false;
        isJumping = false;
        canMove = true;
        canFlip = true;
    }

    /// <summary>
    /// Pone al jugador en la posicion correspondiente al ultimo checkpoint con el que interactuo
    /// </summary>
    private void SetOnLastCheckpointPosition()
    {
        this.transform.position = GameManager.SharedInstance.GetLastCheckpointPosition();
        TeleportVirtualCamera.SharedInstance.ChangePosition(GameManager.SharedInstance.GetLastCheckpointPosition());
        Debug.Log("Posición de reaparición: " + GameManager.SharedInstance.GetLastCheckpointPosition());
    }

    /// <summary>
    /// Pone al jugador en la posicion inicial predeterminada al comenzar el juego
    /// </summary>
    private void SetOnNewGamePosition()
    {
        this.transform.position = GameManager.SharedInstance.newGamePosition;
        Debug.Log("Posición de reaparición: " + GameManager.SharedInstance.newGamePosition);
    }

    /// <summary>
    /// Habilita o deshabilita que el jugador pueda o no controlarse mediante inputs.
    /// </summary>
    /// <param name="isControlable">
    /// <para>True: el jugador es controlable.</para>
    /// <para>False: el jugador no es controlable.</para>
    /// </param>
    public void SetIsControlable(bool isControlable)
    {
        this.isControlable = isControlable;

        if (!this.isControlable)
        {
            StopAllCoroutines();
            _animatorController.Play(AnimationID.Idle);
            _rigidbody.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Establece si el jugador ha muerto o no.
    /// </summary>
    /// <param name="isDead"></param>
    public void SetPlayerIsDead(bool isDead)
    {
        this.isDead = isDead;
    }

    /// <summary>
    /// Da monedas al jugador.
    /// </summary>
    public void GiveCoin()
    {
        Coins = Mathf.Clamp(Coins + 1, 0, 10);
        
        if (Coins == 10)
        {
            HealthManager.Instance.GiveHealthPoint();
            Coins = 0;
        }
    }

    /// <summary>
    /// Cambia el PowerUp del jugador al momento de recoger uno.
    /// </summary>
    /// <param name="powerUpID">Identificador del power up.</param>
    /// <param name="amount">Cantidad de power up recogida.</param>
    public void ChangePowerUp(PowerUpID powerUpID, int amount)
    {
        _currentPowerUp = powerUpID;
        PowerUpAmount = amount;
    }

    /// <summary>
    /// Cambia la posicion del jugador.
    /// </summary>
    /// <param name="position">Nueva posicion.</param>
    public void ChangePosition(Vector2 position)
    {
        _rigidbody.velocity = Vector2.zero;
        this.transform.position = position;
    }

}
