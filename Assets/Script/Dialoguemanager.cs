using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public GameObject dialogueBox;

   


    public void ShowDialogue(string dialogue)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
        StartCoroutine(ClearMessage());
    }

    private IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(6f); // Display the message for 2 seconds
        dialogueBox.SetActive(false);
    }
}
