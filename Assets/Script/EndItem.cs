using UnityEngine;

public class EndItem : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.CheckEndScreen();
        }
    }
}
