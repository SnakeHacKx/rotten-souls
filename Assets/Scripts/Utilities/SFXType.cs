using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Guarda los efectos de sonido en un enumerado.
/// </summary>
public class SFXType : MonoBehaviour
{
    /// <summary>
    /// Guarda los efectos de sonido.
    /// </summary>
    public enum SoundType
    {
        ATTACK, HIT
    }

    public SoundType type;
}
