﻿using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationID
{
    Idle = 0,
    Run = 1,
    PrepareJump = 2,
    Jump = 3,
    Attack = 4,
    Hurt = 5,
    UsePowerUp = 6,
    Rise = 7,
    Walk = 8,
    LookAtTarget = 9,
    Thunder = 10
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

    // Se encarga de reproducir las animaciones
    public void Play(AnimationID animationID)
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        // todo: activar/desactivar animaciones
        _animator.Play(animationID.ToString());
    }

    public void Pause()
    {
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        _animator.speed = 0;
    }

    public void Unpause()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        _animator.speed = 1;
    }
}
