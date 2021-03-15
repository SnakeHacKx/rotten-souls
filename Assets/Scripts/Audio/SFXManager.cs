using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Mánager de los efectos de sonido.</para>
/// Contiene todos los métodos referentes a los efectos de sonido del juego.
/// <list type="bullet">
/// <item>
/// <term>FindAudioSource</term>
/// <description>Actualiza la vida máxima</description>
/// </item>
/// <item>
/// <term>PlaySFX</term>
/// <description>Actualiza la vida máxima</description>
/// </item>
/// </list>
/// </summary>
public class SFXManager : MonoBehaviour
{
    // TRUCO DEL SINGLETON: nos ayuda a que no se pueda instanciar una segunda segunda vez la clase

    private static SFXManager sharedInstance = null; // Instancia compartida

    public static SFXManager SharedInstance { get { return sharedInstance; } }

    // Lista de audios de SFX
    private List<GameObject> audios;

    /// <summary>
    /// Inicializa algunas variables importantes.
    /// </summary>
    private void Awake()
    {
        // Si alguien ya ha configurado esa instancia única compartida entre todo el proyecto
        // Y esa instancia única compartida no soy yo (el GameObject que tiene el script)
        // Es significa que alguien está intentando crear un segundo manager

        if (sharedInstance != null && sharedInstance != this)
        {
            // Si hay alguno más, lo destruyo, sólo puede haber un SFXManager
            Destroy(gameObject);
        }

        sharedInstance = this;

        audios = new List<GameObject>();

        GameObject sounds = GameObject.Find("Sounds");

        // Truco para buscar en los hijos
        // t: son cada uno de los hijos
        foreach (Transform t in sounds.transform)
        {
            audios.Add(t.gameObject);
        }
    }

    /// <summary>
    /// Recupera el componente AudioSource del tipo de audio que recibe la variable <paramref name="type"/>.
    /// </summary>
    /// <param name="type">Recibe el tipo de efecto de sonido a encontrar.</param>
    /// <returns>
    /// Si el tipo es encontrado, devuelve el componente AudioSource del tipo en cuestión.
    /// <para>Si no logra encontrarlo, devuelve un valor nulo.</para>
    /// </returns>
    public AudioSource FindAudioSource(SFXType.SoundType type)
    {
        foreach (GameObject g in audios)
        {
            if(g.GetComponent<SFXType>().type == type)
            {
                return g.GetComponent<AudioSource>();
            }
        }

        return null;
    }

    /// <summary>
    /// Reproduce eñ tipo de efecto de sonido que recibe la variable <paramref name="type"/>.
    /// </summary>
    /// <param name="type">Recibe el tipo de efecto de sonido a reproducir.</param>
    public void PlaySFX(SFXType.SoundType type)
    {
        FindAudioSource(type).Play();
    }
}
