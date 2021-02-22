using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // TRUCO DEL SINGLETON: nos ayuda a que no se pueda instanciar una segunda segunda vez la clase

    private static SFXManager sharedInstance = null; // Instancia compartida

    public static SFXManager SharedInstance { get { return sharedInstance; } }

    private List<GameObject> audios;

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

    public void PlaySFX(SFXType.SoundType type)
    {
        FindAudioSource(type).Play();
    }
}
