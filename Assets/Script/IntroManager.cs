using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // To manage scene transitions
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class IntroController : MonoBehaviour
{
    public UnityEngine.UI.Image blackScreen;
    public AudioSource screamAudio;
    public float fadeDuration = 1f;
    public TMP_Text IntroText;
    private void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        // Ensure the black screen is fully opaque at the start
        blackScreen.color = Color.black;

        // Play the scream audio
        screamAudio.Play();

        // Wait for the audio to finish
        yield return new WaitForSeconds(screamAudio.clip.length);

        // Fade out the black screen
        float elapsedTime = 0f;
        Color screenColor = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            screenColor.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            blackScreen.color = screenColor;
            yield return null;
        }

        // Ensure the black screen is fully transparent
        blackScreen.color = new Color(0, 0, 0, 0);
        IntroText.text = "This place has clearly been abandoned for a while...\r\n\r\n My task is clear however. I need to uncover the truth of what happened here... Let me look around.";
        StartCoroutine(ClearMessage());
    }
    private IEnumerator ClearMessage()
    {
        yield return new WaitForSeconds(5f); // Display the message for 2 seconds
        IntroText.text = "";
    }
}
