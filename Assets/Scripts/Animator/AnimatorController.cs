using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimID
{
    UsePowerUp = 0
}

public enum GlobalAnimID
{
    Idle = 0,
    Run = 1,
    Attack = 2,
    Hurt = 3,
    Walk = 4,
    PrepareJump = 5,
    Jump = 6,
}

public enum AndroMaliusAnimID
{
    LookAtTarget = 0,
    Thunder = 1
}

public enum NormalEnemyAnimID
{
    Rise = 0
}

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    private static AnimatorController _sharedInstance;
    
    public static AnimatorController Instance
    {
        get
        {
            if (_sharedInstance == null)
            {
                _sharedInstance = FindObjectOfType<AnimatorController>();
            }
            return _sharedInstance;
        }
        set
        {
            _sharedInstance = value;
        }
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Se encarga de reproducir las animaciones
    /// </summary>
    public void Play(string animationID)
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        // todo: activar/desactivar animaciones
        _animator.Play(animationID);
    }

    /// <summary>
    /// Pausa una animacion
    /// </summary>
    public void Pause()
    {
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        _animator.speed = 0;
    }

    /// <summary>
    /// Reanuda una animacion
    /// </summary>
    public void Unpause()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        _animator.speed = 1;
    }
}
