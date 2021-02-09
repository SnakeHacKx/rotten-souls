using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class GoToNewPlace : MonoBehaviour
{
    [SerializeField]
    private string newPlaceName = "New Scene Name Here!!!";

    private SetCameraConfiner _setCameraConfiner;

    private void Start()
    {
        _setCameraConfiner = GetComponent<SetCameraConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el tag del gameObject que entra en collision con el collider que tenga
        // este script es "Player", entonces cargo una nueva escena
        if (collision.gameObject.CompareTag("Player")/* && Input.GetKeyDown(KeyCode.E)*/)
        {
            
            SceneManager.LoadScene(newPlaceName);
            _setCameraConfiner.SetCameraBoundary();
        }
    }
}
