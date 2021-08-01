using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] private int _initialHealh;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth; // variable de apollo
    
    [Header("Audio")]
    [SerializeField] private AudioClip healthUpSFX;

    private static HealthManager _sharedInstance;

    public static HealthManager Instance
    {
        get
        {
            if (_sharedInstance == null)
            {
                _sharedInstance = FindObjectOfType<HealthManager>();
            }
            return _sharedInstance;
        }
        set
        {
            _sharedInstance = value;
        }
    }

    public int InitialHealth
    {
        get
        {
            return _initialHealh;
        }
        set
        {
            if (value < 0)
            {
                _initialHealh = 0;
            }
            else
            {
                _initialHealh = value;
            }
        }
    }

    public int Health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (value < 0)
            {
                _currentHealth = 0;
            }
            else
            {
                _currentHealth = value;
            }
        }
    }

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if (value < 0)
            {
                _maxHealth = 0;
            }
            else
            {
                _maxHealth = value;
            }
        }
    }

    /// <summary>
    /// Da un punto de vida al GameObject.
    /// </summary>
    public void GiveHealthPoint()
    {
        Health = Mathf.Clamp(Health + 1, 0, MaxHealth);
        AudioManager.SharedInstance.PlaySFX(healthUpSFX);
    }
}
