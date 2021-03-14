using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class NPCDialogue : MonoBehaviour
{
    [SerializeField]
    private string npcName;

    [SerializeField]
    private string[] npcDialogueLines;

    private DialogueManager dialogueManager;
    private bool playerInTheZone;

    void Start()
    {
        // recordar que esto sólo se puede hacer la siguiente línea, si y sólo si hay un solo
        // objeto de una categoría en la escena... en este caso solamente hay un objeto que
        // tiene el DialogueManager
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInTheZone = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInTheZone = false;
        }
    }

    void Update()
    {
        if(playerInTheZone && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Joystick1Button3)))
        {
            string[] finalDialogue = new string[npcDialogueLines.Length];

            int i = 0;
            // Para cada línea de diálogo en npcDialogueLines
            foreach(string line in npcDialogueLines)
            {
                finalDialogue[i++] = ((npcName != null) ? npcName + "\n" : "") + line;
            }

            dialogueManager.ShowDialogue(finalDialogue);
        }
    }
}
