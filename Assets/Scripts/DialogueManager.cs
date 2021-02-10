using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(TMP_Text))]
[RequireComponent(typeof(PlayerController))]
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueText;

    [SerializeField]
    [Tooltip("El diálogo está activo?")]
    private bool dialogueActive;

    [SerializeField]
    [Tooltip("Las diferentes líneas de diálogo que puede tener NPC, objeto, etc...")]
    private string[] dialogueLines;

    [SerializeField]
    [Tooltip("Muestra la línea de diálogo actual")]
    private int currentDialogueLine;

    private PlayerController playerController;

    private void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);

        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueLine++;

            if (currentDialogueLine >= dialogueLines.Length)
            {
                dialogueActive = false;
                dialogueBox.SetActive(false);
                currentDialogueLine = 0;
                playerController.isTalking = false;
            }
            else
            {
                dialogueText.text = dialogueLines[currentDialogueLine];
            }
        }
    }

    public void ShowDialogue(string[] lines)
    {
        currentDialogueLine = 0;
        dialogueLines = lines;
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogueLines[currentDialogueLine];
        playerController.isTalking = true;
    }
}
