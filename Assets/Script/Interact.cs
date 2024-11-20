using UnityEngine;

public class Interactable : MonoBehaviour
{
    [TextArea]
    public string dialogue;
    private bool isPressed = false; // To ensure each item is counted only once

    private void OnMouseDown()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.ShowDialogue(dialogue);
        }

        if (!isPressed)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.ItemPressed();
                isPressed = true;
            }
        }
    }
}
