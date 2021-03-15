using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ...
/// </summary>
public class PlayNewTrack : MonoBehaviour
{
    private AudioManager audioManager;
    public int newTrackID;

    public bool playOnStart;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        
        if (playOnStart)
        {
            audioManager.PlayNewTrack(newTrackID);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagID.Player.ToString()))
        {
            audioManager.PlayNewTrack(newTrackID);
            gameObject.SetActive(false);
        }
    }
}
