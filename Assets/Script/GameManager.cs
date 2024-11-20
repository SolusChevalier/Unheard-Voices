using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI; // Assuming you're using UI elements for the end screen and messages
using System.Collections; // Add this directive to use IEnumerator
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalItemsToPress = 6;
    private int itemsPressed = 0;
    public GameObject endScreen;
    public TMP_Text messageText; // Text to display messages like "You can't press this yet"

    private void Start()
    {
        endScreen.SetActive(false); // Hide the end screen initially
    }

    public void ItemPressed()
    {
        itemsPressed++;
        if (itemsPressed >= totalItemsToPress)
        {
            // All items have been pressed
            UnityEngine.Debug.Log("All items pressed");
            messageText.text = "That seems to be all there is to see here...\r\n\r\n I should go look upstairs";
            StartCoroutine(ClearMessage());
        }
    }

    public void CheckEndScreen()
    {
        if (itemsPressed >= totalItemsToPress)
        {
            endScreen.SetActive(true);
            StartCoroutine(EndWait());
        }
        else
        {
            messageText.text = "Theres still more to see...";
            StartCoroutine(ClearMessage());
        }
    }

    private IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(5f); // Display the message for 2 seconds
        messageText.text = "";
    }

    private IEnumerator EndWait()
    {
        yield return new WaitForSeconds(5f); // Display the message for 2 seconds
        messageText.text = "";
        SceneManager.LoadScene("Menu");
    }
}