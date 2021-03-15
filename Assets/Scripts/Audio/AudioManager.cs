using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Mánager del audio.</para>
/// Contiene todos los métodos referentes al audio del juego.
/// <list type="bullet">
/// <item>
/// <term>Update</term>
/// <description>Analiza si el audio puede ser reproducido y lo hace o no según el resultado.</description>
/// </item>
/// <item>
/// <term>PlayNewTrack</term>
/// <description>Reproduce un nuevo track.</description>
/// </item>
/// </list>
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Tooltip("Inserte los tracks que desea reproducir")]
    [SerializeField] private AudioSource[] audioTracks;
    // Track actual
    public int currentTrack;
    // Variable booleana que permite saber si la canción puede o no sonar en determinada zona o momento
    public bool audioCanBePlayed;

    /// <summary>
    /// Analiza si el audio puede ser reproducido y lo hace o no según el resultado.
    /// <para>Si no puede reproducirlo, detiene el audio en reproducción</para>
    /// </summary>
    private void Update()
    {
        if (audioCanBePlayed)
        {
            if (!audioTracks[currentTrack].isPlaying)
            {
                audioTracks[currentTrack].Play();
            }
        }
        else
        {
            audioTracks[currentTrack].Stop();
        }
    }

    /// <summary>
    /// Reproduce un nuevo track.
    /// </summary>
    /// <param name="newTrack">Nuevo track a reproducir.</param>
    public void PlayNewTrack(int newTrack)
    {
        audioTracks[currentTrack].Stop();
        currentTrack = newTrack;
        audioTracks[currentTrack].Play();
    }
}
